using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockAnalyzer
{
    internal enum InvestmentEnum
    {
        STOCKS,
        COINS
    }

    internal class PriceBaseType
    {
        public DateTime dateTime;
        public double price;
        public int volume;
    }


    internal class BaseInvestmentType
    {
        public string name { get; set; }
        public InvestmentEnum investmentType;
        public List<PriceBaseType> listPriceData = new List<PriceBaseType>();
    }
}
