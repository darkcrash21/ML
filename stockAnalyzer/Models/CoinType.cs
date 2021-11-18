using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockAnalyzer
{
    public class PriceCoinType : PriceBaseType
    {
        public UInt64 marketCap = 0;
        public string circulatingSupply = String.Empty;
    }

    public class CoinType : BaseInvestmentType
    {
        new List<PriceCoinType> listPriceData = new List<PriceCoinType>();
    }
}
