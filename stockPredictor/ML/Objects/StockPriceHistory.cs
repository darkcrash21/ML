using Microsoft.ML.Data;
using System;

namespace stockPredictor.ML.Objects
{
   public class StockPriceHistory
   {
      [LoadColumn(0)]
      public DateTime DateTime { get; set; }

      [LoadColumn(1)]
      public float Price { get; set; }

      [LoadColumn(2)]
      public float MarketCap { get; set; }

      [LoadColumn(3)]
      public float Volume { get; set; }

      //[LoadColumn(9)]
      //public string CirculatingSupply { get; set; }

   }
}