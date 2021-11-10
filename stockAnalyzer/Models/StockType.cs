using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockAnalyzer
{
    internal class PriceStockType : PriceBaseType
    {
        public double priceHigh;
        public double priceLow;
    }

    internal class StockType : BaseInvestmentType
    {
        new List<PriceStockType> listPriceData = new List<PriceStockType>();
    }
}
