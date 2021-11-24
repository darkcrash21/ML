using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockAnalyzer
{
   public enum InvestmentEnum
   {
      INVALID,
      STOCKS,
      COINS
   } // InvestmentEnum

   public class PriceBaseType
   {
      public DateTime dateTime;
      public double price;
      public UInt64 volume;
   } // PriceBaseType

   public class BaseInvestmentType
   {
      public string name = String.Empty;
      public InvestmentEnum investmentType = InvestmentEnum.INVALID;
      public List<PriceBaseType> listPriceData = new List<PriceBaseType>();
   } // BaseInvestmentType
}
