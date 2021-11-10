using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockAnalyzer
{

    internal class PriceCoinType : PriceBaseType
    {
        public int marketCap;
        public string circulatingSupply;
    }

    internal class CoinType : BaseInvestmentType
    {
        new List<PriceCoinType> listPriceData = new List<PriceCoinType>();
    }
}
