using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockAnalyzer
{
   public class PriceStockType : PriceBaseType
   {
      public double dailyHigh;
      public double dailyLow;
   }

   public class StockType : BaseInvestmentType
   {
      new List<PriceStockType> listPriceData = new List<PriceStockType>();
   }
}
