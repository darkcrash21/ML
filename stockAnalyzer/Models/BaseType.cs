using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockAnalyzer
{
    internal enum InvestmentEnum
    {
        INVALID,
        STOCKS,
        COINS
    } // InvestmentEnum

    internal class PriceBaseType
    {
        public DateTime dateTime;
        public double price;
        public int volume;
    } // PriceBaseType

    internal class BaseInvestmentType
    {
        public string name = String.Empty;
        public InvestmentEnum investmentType = InvestmentEnum.INVALID;
        public List<PriceBaseType> listPriceData = new List<PriceBaseType>();
    } // BaseInvestmentType
}
