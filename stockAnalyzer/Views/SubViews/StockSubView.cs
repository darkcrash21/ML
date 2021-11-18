using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stockAnalyzer
{
    public partial class StockSubView : BaseSubView
    {
        private StockType stockData;

        //
        // Constructor
        //
        #region CONSTRUCTOR_DESTRUCTOR
        public StockSubView(StockType stockData) : base(stockData)
        {
            InitializeComponent();
            this.stockData = stockData;
        } // Constructor
        #endregion CONSTRUCTOR_DESTRUCTOR

        //
        // UI Events
        //
        #region UI_EVENTS
        private void StockSubView_Load(object sender, EventArgs e)
        {
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

            // Add all the data to the list view List<PriceStockType> listPriceData
            List<List<string>> listRows = new List<List<string>>();
            foreach (PriceStockType priceData in this.stockData.listPriceData)
            {
                string dateTime = priceData.dateTime.ToString("g");
                string price = priceData.price.ToString("N" + this.GetNumDecimalPlaces(priceData.price));
                string volume = priceData.volume.ToString("N" + this.GetNumDecimalPlaces(priceData.volume));
                string dailyHigh = priceData.dailyHigh.ToString("N" + this.GetNumDecimalPlaces(priceData.price));
                string dailyLow = priceData.dailyLow.ToString("N" + this.GetNumDecimalPlaces(priceData.price));
                List<string> listRow = new List<string>();
                listRow.Add(dateTime);
                listRow.Add(price);
                listRow.Add(volume);
                listRow.Add(dailyLow);
                listRow.Add(dailyHigh);

                listRows.Add(listRow);
            }
            this.AddDataRows(listRows);
        } // StockSubView_Load()
        #endregion UI_EVENTS
    }
}
