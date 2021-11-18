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
    public partial class CoinSubView : BaseSubView
    {
        private CoinType coinData;

        //
        // Constructor
        //
        #region CONSTRUCTOR_DESTRUCTOR
        public CoinSubView(CoinType coinData) : base(coinData)
        {
            InitializeComponent();
            this.coinData = coinData;
        } // Constructor
        #endregion CONSTRUCTOR_DESTRUCTOR

        //
        // UI Events
        //
        #region UI_EVENTS
        private void CoinSubView_Load(object sender, EventArgs e)
        {
            ColumnHeader chMarketCap = new ColumnHeader();
            chMarketCap.Text = "Market Cap";
            chMarketCap.Name = "chMarketCap";
            chMarketCap.Width = 150;
            this.AddColumnHeader(chMarketCap);

            ColumnHeader chCirculatingSupply = new ColumnHeader();
            chCirculatingSupply.Text = "Circ. Supply";
            chCirculatingSupply.Name = "chCirculatingSupply";
            chCirculatingSupply.Width = 100;
            this.AddColumnHeader(chCirculatingSupply);

            // Add all the data to the list view List<CoinType> listPriceData
            List<List<string>> listRows = new List<List<string>>();
            foreach (PriceCoinType priceData in this.coinData.listPriceData)
            {
                string dateTime = priceData.dateTime.ToString("g");
                string price = priceData.price.ToString("N" + this.GetNumDecimalPlaces(priceData.price));
                string volume = priceData.volume.ToString("N" + this.GetNumDecimalPlaces(priceData.volume));
                string marketCap = priceData.marketCap.ToString("N" + this.GetNumDecimalPlaces(priceData.marketCap));
                string circSupply = priceData.circulatingSupply;
                List<string> listRow = new List<string>();
                listRow.Add(dateTime);
                listRow.Add(price);
                listRow.Add(volume);
                listRow.Add(marketCap);
                listRow.Add(circSupply);

                listRows.Add(listRow);
            }
            this.AddDataRows(listRows);
        } // CoinSubView_Load()
        #endregion UI_EVENTS
    }
}
