using Microsoft.ML.Data;

namespace stockPredictor.ML.Objects
{
   public class StockPricePrediction
   {
      [ColumnName("Score")]
      public float Price;
   }
}