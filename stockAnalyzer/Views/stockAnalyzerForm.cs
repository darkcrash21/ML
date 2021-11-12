using System.Globalization;

namespace stockAnalyzer
{
    public partial class stockAnalyzerForm : Form
    {
        //
        // Attributes
        //
        #region ATTRIBUTES

        private int totalLinesForProgress = 0;
        private int currentLinesForProgress = 0;

        // Data containers
        private static Dictionary<string, BaseInvestmentType> dictInvestment2Data = new Dictionary<string, BaseInvestmentType>();

        // Threads
        private List<Thread> listThreads = new List<Thread>();
        #endregion ATTRIBUTES

        //
        // Constructor / Destructor
        //
        #region CONSTRUCTOR_DESTRUCTOR
        public stockAnalyzerForm()
        {
            InitializeComponent();
        } // stockAnalyzerForm()
        #endregion CONSTRUCTOR_DESTRUCTOR

        //
        // Form Events
        //
        #region FORM_EVENTS
        #endregion FORM_EVENTS

        //
        // UI Events
        //
        #region UI_EVENTS
        private void tsmiOpenDataDirectory_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    List<Tuple<string, DateTime>> listDates = new List<Tuple<string, DateTime>>();
                    string[] dateDirectories = Directory.GetDirectories(fbd.SelectedPath);

                    // Only get the directories with dates
                    foreach (string dateDirectory in dateDirectories)
                    {
                        string dirName = dateDirectory.Substring(dateDirectory.LastIndexOf('\\') + 1);
                        try
                        {
                            DateTime dt = DateTime.ParseExact(dirName, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                            listDates.Add(new Tuple<string, DateTime>(dateDirectory, dt));
                        }
                        catch { }
                    }

                    // Sort Directories by Date
                    listDates.Sort(delegate (Tuple<string, DateTime> x, Tuple<string, DateTime> y)
                    {
                        if (x.Item2 < y.Item2) 
                        {
                            return 1; 
                        }
                        else
                        {
                            return -1;
                        }
                    });

                    this.lvDates.Items.Clear();
                    foreach(Tuple<string, DateTime> tupDir in listDates)
                    {
                        ListViewItem item = new ListViewItem(tupDir.Item2.ToString("MM-dd-yyy"));
                        item.Tag = tupDir.Item1;
                        this.lvDates.Items.Add(item);
                    }
                } // ok
            } // using fbd
        } // tsmiOpenDataDirectory_Click()

        private void btnProcessDates_Click(object sender, EventArgs e)
        {
            // Reset all global attributes
            this.ResetAll();

            List<ListViewItem> listLvItemsReversed = new List<ListViewItem>();
            foreach (ListViewItem item in this.lvDates.Items)
            {
                if (item.Checked)
                {
                    listLvItemsReversed.Add(item);
                }
            }
            listLvItemsReversed.Reverse();

            foreach(ListViewItem item in listLvItemsReversed)
            { 
                string directoryPath = (string)item.Tag;
                string[] files = Directory.GetFiles(directoryPath, "*.dat", SearchOption.AllDirectories);

                // Figure out the total progress
                this.currentLinesForProgress = 0;
                foreach (string file in files)
                {
                    this.totalLinesForProgress = File.ReadAllLines(file).Length;
                } // for each file

                this.lblProgress.Text = "Processing " + directoryPath.Substring(directoryPath.LastIndexOf('\\') + 1);

                // Actually parse the files
                foreach (string file in files)
                {
                    string type = file.Substring(file.LastIndexOf("\\") + 1);
                    if (type.StartsWith("Stocks"))
                    {
                        this.ParseStockFileThread(file);
                    }
                    else if (type.StartsWith("Coin"))
                    {
                        this.ParseCoinFileThread(file);
                    }
                } // for each file
            }

            this.lblProgress.Text = "Displaying Data";

            foreach(KeyValuePair<string, BaseInvestmentType> kvPair in dictInvestment2Data)
            {
                this.CreateInvestmentTab(kvPair.Value);
            }
        } // btnProcessDates_Click()

        private void IncrementProgress()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => this.IncrementProgress()));
            }
            else
            {
                this.currentLinesForProgress++;

                this.progressBar.Value = Math.Min(100, (int)((double)(this.currentLinesForProgress) / (double)(this.totalLinesForProgress) * 100.0));
            }
        } // IncrementProgress()
        #endregion UI_EVENTS

        //
        // Async Methods
        //
        #region ASYNC_METHODS
        private void ParseStockFileThread(string file)
        {
            StockType stockData = new StockType();
            string stockName = file.Substring(file.LastIndexOf("\\") + 1).Replace("Stocks", "").Replace(".dat", "");


            using (StreamReader sr = new StreamReader(file))
            {
                // skip the header
                string line = sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    IncrementProgress();
                    line = sr.ReadLine();

                    string[] lineSplit = line.Split(',', StringSplitOptions.RemoveEmptyEntries);

                    if (lineSplit.Length == 5)
                    {
                        PriceStockType priceData = new PriceStockType();

                        priceData.dateTime = DateTime.ParseExact(lineSplit[0], "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);

                        double.TryParse(lineSplit[1], out priceData.price);
                        double.TryParse(lineSplit[2], out priceData.priceHigh);
                        double.TryParse(lineSplit[3], out priceData.priceLow);
                        int.TryParse(lineSplit[4], out priceData.volume);

                        if (dictInvestment2Data.ContainsKey(stockName))
                        {
                            stockData = (StockType)dictInvestment2Data[stockName];
                        }
                        else
                        {
                            stockData.name = stockName;
                            stockData.investmentType = InvestmentEnum.STOCKS;
                            dictInvestment2Data.Add(stockName, stockData);
                        }

                        stockData.listPriceData.Add(priceData);
                    }
                    else
                    {
                        Console.WriteLine("Error: ParseStockFileThread - " + line);
                    }
                }
            }
        } // ParseStockFileThread()

        private void ParseCoinFileThread(string file)
        {
            CoinType coinData = new CoinType();
            string coinName = file.Substring(file.LastIndexOf("\\") + 1).Replace("Coin", "").Replace(".dat", "");

            using (StreamReader sr = new StreamReader(file))
            {
                // skip the header
                string line = sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    IncrementProgress();
                    line = sr.ReadLine();

                    string[] lineSplit = line.Split(',', StringSplitOptions.RemoveEmptyEntries);

                    if (lineSplit.Length == 5)
                    {
                        PriceCoinType priceData = new PriceCoinType();

                        priceData.dateTime = DateTime.ParseExact(lineSplit[0], "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);

                        double.TryParse(lineSplit[1], out priceData.price);
                        int.TryParse(lineSplit[2], out priceData.marketCap);
                        int.TryParse(lineSplit[3], out priceData.volume);
                        priceData.circulatingSupply = lineSplit[4];

                        if (dictInvestment2Data.ContainsKey(coinName))
                        {
                            coinData = (CoinType)dictInvestment2Data[coinName];
                        }
                        else
                        {
                            coinData.name = coinName;
                            coinData.investmentType = InvestmentEnum.COINS;
                            dictInvestment2Data.Add(coinName, coinData);
                        }

                        coinData.listPriceData.Add(priceData);
                    }
                    else
                    {
                        Console.WriteLine("Error: ParseCoinFileThread - " + line);
                    }
                }
            }

            //this.CreateInvestmentTab(coinData);
        } // ParseCoinFileThread()

        private void CreateInvestmentTab(BaseInvestmentType baseData)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => CreateInvestmentTab(baseData)));
            }
            else
            {
                if (baseData.investmentType == InvestmentEnum.STOCKS)
                {
                    StockType stockData = (StockType)baseData;
                    TabPage tabPage = new TabPage(stockData.name);
                    this.tcStocks.TabPages.Add(tabPage);
                }
                else if (baseData.investmentType == InvestmentEnum.COINS)
                {
                    CoinType coinData = (CoinType)baseData;
                    TabPage tabPage = new TabPage(coinData.name);
                    this.tcCoins.TabPages.Add(tabPage);
                }
            }
        } // CreateInvestmentTab()

        #endregion ASYNC_METHODS

        //
        // Misc Methods
        //
        #region MISC_METHODS
        private void ResetAll()
        {
            this.totalLinesForProgress = 0;
            this.currentLinesForProgress = 0;
            dictInvestment2Data = new Dictionary<string, BaseInvestmentType>();
        } // ResetAll()
        #endregion MISC_METHODS

    }
}