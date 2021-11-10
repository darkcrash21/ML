using System.Globalization;

namespace stockAnalyzer
{
    public partial class stockAnalyzerForm : Form
    {
        //
        // Attributes
        //
        #region ATTRIBUTES

        private static int totalLinesForProgress = 0;
        private static int currentLinesForProgress = 0;

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
        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    this.tbxPath.Text = fbd.SelectedPath;

                    string[] files = Directory.GetFiles(fbd.SelectedPath, "*.dat", SearchOption.AllDirectories);

                    // Figure out the total progress
                    foreach (string file in files)
                    {
                        totalLinesForProgress = File.ReadAllLines(file).Length;
                    } // for each file

                    // Actually parse the files
                    foreach (string file in files)
                    {
                        string type = file.Substring(file.LastIndexOf("\\") + 1);
                        if (type.StartsWith("Stocks"))
                        {
                            Thread th = new Thread(() => ParseStockFileThread(file));
                            th.Start();
                            this.listThreads.Add(th);
                        }
                        else if (type.StartsWith("Coin"))
                        {
                            Thread th = new Thread(() => ParseCoinFileThread(file));
                            th.Start();
                            this.listThreads.Add(th);
                        }
                    } // for each file
                } // ok
            } // using fbd
        } // btnOpen_Click()

        private void IncrementProgress()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => this.IncrementProgress()));
            }
            else
            {
                currentLinesForProgress++;

                this.progressBar.Value = Math.Min(100, (int)((double)(currentLinesForProgress) / (double)(totalLinesForProgress) * 100.0));
            }
        }
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
                IncrementProgress();

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    IncrementProgress();

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

            this.CreateInvestmentTab(stockData);
        } // ParseStockFileThread()

        private void ParseCoinFileThread(string file)
        {
            CoinType coinData = new CoinType();
            string coinName = file.Substring(file.LastIndexOf("\\") + 1).Replace("Coin", "").Replace(".dat", "");

            using (StreamReader sr = new StreamReader(file))
            {
                // skip the header
                string line = sr.ReadLine();
                IncrementProgress();

                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    IncrementProgress();

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

            this.CreateInvestmentTab(coinData);
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
            totalLinesForProgress = 0;
            currentLinesForProgress = 0;
            dictInvestment2Data = new Dictionary<string, BaseInvestmentType>();
        } // ResetAll()
        #endregion MISC_METHODS
    }
}