using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stockAnalyzer
{
    public partial class StockSubView : BaseSubView
    {
        private StockType stockData;
        
        private Pen penPrice = new Pen(Color.Blue, 2);
        private Pen penDailyHigh = new Pen(Color.Green, 2);
        private Pen penDailyLow = new Pen(Color.Red, 2);

        private DashStyle dashStylePrice = DashStyle.Solid;
        private DashStyle dashStyleDailyHigh = DashStyle.Dash;
        private DashStyle dashStyleDailyLow = DashStyle.DashDot; 

        //
        // Constructor
        //
        #region CONSTRUCTOR_DESTRUCTOR
        public StockSubView(StockType stockData) : base(stockData)
        {
            InitializeComponent();
            this.stockData = stockData;

            penPrice.DashStyle = this.dashStylePrice;
            penDailyHigh.DashStyle = this.dashStyleDailyHigh;
            penDailyLow.DashStyle = this.dashStyleDailyLow;
        } // Constructor
        #endregion CONSTRUCTOR_DESTRUCTOR

        //
        // UI Events
        //
        #region UI_EVENTS
        private void StockSubView_Load(object sender, EventArgs e)
        {
            // Add the columns
            ColumnHeader chDailyLow = new ColumnHeader();
            chDailyLow.Text = "Daily Low";
            chDailyLow.Name = "chDailyLow";
            chDailyLow.Width = 100;
            this.AddColumnHeader(chDailyLow);

            ColumnHeader chDailyHigh = new ColumnHeader();
            chDailyHigh.Text = "Daily High";
            chDailyHigh.Name = "chDailyHigh";
            chDailyHigh.Width = 100;
            this.AddColumnHeader(chDailyHigh);

            // Create the graph data objects
            GraphDataType priceGraphData = new GraphDataType("Price", this.penPrice);
            GraphDataType dailyHighGraphData = new GraphDataType("Daily High", this.penDailyHigh);
            GraphDataType dailyLowGraphData = new GraphDataType("Daily Low", this.penDailyLow);

            // Add all the data to the list view and graph
            List<List<string>> listRows = new List<List<string>>();
            List<GraphDataType> listGraphData = new List<GraphDataType>();
            listGraphData.Add(priceGraphData);
            listGraphData.Add(dailyHighGraphData);
            listGraphData.Add(dailyLowGraphData);

            foreach (PriceStockType priceData in this.stockData.listPriceData)
            {
                // Add to the details rows
                string dateTime = priceData.dateTime.ToString("g");
                string price = priceData.price.ToString("N" + this.GetNumDecimalPlaces(priceData.price));
                string volume = priceData.volume.ToString("N" + this.GetNumDecimalPlaces(priceData.volume));
                string dailyHigh = priceData.dailyHigh.ToString("N" + this.GetNumDecimalPlaces(priceData.dailyHigh));
                string dailyLow = priceData.dailyLow.ToString("N" + this.GetNumDecimalPlaces(priceData.dailyLow));
                List<string> listRow = new List<string>();
                listRow.Add(dateTime);
                listRow.Add(price);
                listRow.Add(volume);
                listRow.Add(dailyLow);
                listRow.Add(dailyHigh);

                listRows.Add(listRow);

                // Add to the graph
                priceGraphData.AddValue(priceData.price);
                dailyHighGraphData.AddValue(priceData.dailyHigh);
                dailyLowGraphData.AddValue(priceData.dailyLow);
            }
            
            this.AddDataRows(listRows);

            // Scale the graph data
            priceGraphData.ScaleGraphPoints();
            dailyHighGraphData.ScaleGraphPoints(priceGraphData.MinBuffer(), priceGraphData.MaxBuffer());
            dailyLowGraphData.ScaleGraphPoints(priceGraphData.MinBuffer(), priceGraphData.MaxBuffer());
            this.AddGraphData(listGraphData);
        } // StockSubView_Load()
        #endregion UI_EVENTS
    }
}
