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

      private Pen penPrice = new Pen(Color.Green, 2.5f);
      private Pen penDailyHigh = new Pen(Color.FromArgb((int)(255 * .5), Color.Blue), 1.5f);
      private Pen penDailyLow = new Pen(Color.FromArgb((int)(255 * .5), Color.Red), 1.5f);
      private Pen penPriceBestFit = new Pen(Color.FromArgb((int)(255 * .75), Color.Green), 1.5f);

      private DashStyle dashStylePrice = DashStyle.Solid;
      private DashStyle dashStyleDailyHigh = DashStyle.Dash;
      private DashStyle dashStyleDailyLow = DashStyle.Dash;
      private DashStyle dashStylePriceBestFit = DashStyle.Dot;

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
         penPriceBestFit.DashStyle = this.dashStylePriceBestFit;
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
         GraphDataType priceBestFitGraphData = new GraphDataType("Price Best Fit", this.penPriceBestFit);

         // Add all the data to the list view and graph
         List<List<string>> listRows = new List<List<string>>();
         List<GraphDataType> listGraphData = new List<GraphDataType>();
         listGraphData.Add(priceGraphData);
         listGraphData.Add(dailyHighGraphData);
         listGraphData.Add(dailyLowGraphData);
         listGraphData.Add(priceBestFitGraphData);

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
         priceGraphData.SetMinMaxBuffers();
         priceGraphData.ScaleGraphPoints();
         dailyHighGraphData.ScaleGraphPoints(priceGraphData.MinBuffer(), priceGraphData.MaxBuffer());
         dailyLowGraphData.ScaleGraphPoints(priceGraphData.MinBuffer(), priceGraphData.MaxBuffer());

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
      } // StockSubView_Load()
      #endregion UI_EVENTS
   }
}
