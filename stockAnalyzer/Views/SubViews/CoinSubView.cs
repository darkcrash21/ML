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
   public partial class CoinSubView : BaseSubView
   {
      private CoinType coinData;

      private Pen penPrice = new Pen(Color.Green, 2.5f);
      //private Pen penCirculatingSupply = new Pen(Color.FromArgb((int)(255 * .5), Color.Blue), 1.5f);
      private Pen penMarketCap = new Pen(Color.FromArgb((int)(255 * .5), Color.Red), 1.5f);
      private Pen penPriceBestFit = new Pen(Color.FromArgb((int)(255 * .75), Color.Green), 1.5f);

      private DashStyle dashStylePrice = DashStyle.Solid;
      //private DashStyle dashStyleCirculatingSupply = DashStyle.Dash;
      private DashStyle dashStyleMarketCap = DashStyle.Dash;
      private DashStyle dashStylePriceBestFit = DashStyle.Dot;

      //
      // Constructor
      //
      #region CONSTRUCTOR_DESTRUCTOR
      public CoinSubView(CoinType coinData) : base(coinData)
      {
         InitializeComponent();
         this.coinData = coinData;

         penPrice.DashStyle = this.dashStylePrice;
         //penCirculatingSupply.DashStyle = this.dashStyleCirculatingSupply;
         penMarketCap.DashStyle = this.dashStyleMarketCap;
         penPriceBestFit.DashStyle = this.dashStylePriceBestFit;
      } // Constructor
      #endregion CONSTRUCTOR_DESTRUCTOR

      //
      // UI Events
      //
      #region UI_EVENTS
      private void CoinSubView_Load(object sender, EventArgs e)
      {
         ColumnHeader chCirculatingSupply = new ColumnHeader();
         chCirculatingSupply.Text = "Circ. Supply";
         chCirculatingSupply.Name = "chCirculatingSupply";
         chCirculatingSupply.Width = 100;
         this.AddColumnHeader(chCirculatingSupply);

         ColumnHeader chMarketCap = new ColumnHeader();
         chMarketCap.Text = "Market Cap";
         chMarketCap.Name = "chMarketCap";
         chMarketCap.Width = 150;
         this.AddColumnHeader(chMarketCap);

         // Create the graph data objects
         GraphDataType priceGraphData = new GraphDataType("Price", this.penPrice);
         //GraphDataType circulatingSupplyGraphData = new GraphDataType("Circulating Supply", this.penCirculatingSupply);
         GraphDataType marketCapGraphData = new GraphDataType("Market Cap", this.penMarketCap);
         GraphDataType priceBestFitGraphData = new GraphDataType("Price Best Fit", this.penPriceBestFit);

         // Add all the data to the list view List<CoinType> listPriceData
         List<List<string>> listRows = new List<List<string>>();
         List<GraphDataType> listGraphData = new List<GraphDataType>();
         listGraphData.Add(priceGraphData);
         //listGraphData.Add(circulatingSupplyGraphData);
         listGraphData.Add(marketCapGraphData);
         listGraphData.Add(priceBestFitGraphData);

         foreach (PriceCoinType priceData in this.coinData.listPriceData)
         {
            string dateTime = priceData.dateTime.ToString("g");
            string price = priceData.price.ToString("N" + this.GetNumDecimalPlaces(priceData.price));
            string volume = priceData.volume.ToString("N" + this.GetNumDecimalPlaces(priceData.volume));
            string circSupply = priceData.circulatingSupply;
            string marketCap = priceData.marketCap.ToString("N" + this.GetNumDecimalPlaces(priceData.marketCap));
            List<string> listRow = new List<string>();
            listRow.Add(dateTime);
            listRow.Add(price);
            listRow.Add(volume);
            listRow.Add(circSupply);
            listRow.Add(marketCap);

            listRows.Add(listRow);

            // Add to the graph
            priceGraphData.AddValue(priceData.price);
            //circulatingSupplyGraphData.AddValue(priceData.circulatingSupply);
            marketCapGraphData.AddValue(priceData.marketCap);
         }
         this.AddDataRows(listRows);

         // Scale the graph data
         priceGraphData.SetMinMaxBuffers();
         priceGraphData.ScaleGraphPoints();
         //circulatingSupplyGraphData.ScaleGraphPoints();
         marketCapGraphData.SetMinMaxBuffers();
         marketCapGraphData.ScaleGraphPoints(marketCapGraphData.MinBuffer(), marketCapGraphData.MaxBuffer());

         // Calculate Price's best fit line
         List<PointF> listOrigPoints = new List<PointF>();
         foreach (GraphDataPointType dataPoint in priceGraphData.GetPoints())
         {
            PointF point = new PointF(dataPoint.x, dataPoint.value);
            listOrigPoints.Add(point);
         }

         double a, b;
         List<PointF> bestFit = MathUtilities.GenerateLinearBestFit(listOrigPoints, out a, out b);
         for (int i = 0; i < bestFit.Count; i++)
         {
            priceBestFitGraphData.AddValue(bestFit[i].Y);
         }
         priceBestFitGraphData.ScaleGraphPoints(priceGraphData.MinBuffer(), priceGraphData.MaxBuffer());

         this.AddGraphData(listGraphData);

      } // CoinSubView_Load()
      #endregion UI_EVENTS
   }
}
