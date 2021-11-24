using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockAnalyzer
{
   public static class MathUtilities
   {
      public static List<PointF> GenerateLinearBestFit(List<PointF> points, out double a, out double b)
      {
         int numPoints = points.Count;
         double meanX = points.Average(point => point.X);
         double meanY = points.Average(point => point.Y);

         double sumXSquared = points.Sum(point => point.X * point.X);
         double sumXY = points.Sum(point => point.X * point.Y);

         a = (sumXY / numPoints - meanX * meanY) / (sumXSquared / numPoints - meanX * meanX);
         b = (a * meanX - meanY);

         double a1 = a;
         double b1 = b;

         return points.Select(point => new PointF() { X = point.X, Y = (float)a1 * point.X - (float)b1 }).ToList();
      } // GenerateLinearBestFit()

      public static List<double> InterpolateValues(double startingValue, double endingValue, double interpSteps)
      {
         List<double> listInterpValues = new List<double>();

         double offset = (endingValue - startingValue) / (double)interpSteps;

         for (int i = 1; i < (int)interpSteps; i++)
         {
            listInterpValues.Add(startingValue + (i * offset));
         }

         return listInterpValues;
      } // InterpolateValues()

      public static List<PriceStockType> InterpolatePriceStockData(PriceStockType prevStockData, PriceStockType curStockData, int interpDurationMinutes)
      {
         List<PriceStockType> listInterpData = new List<PriceStockType>();

         double dMinutes = (curStockData.dateTime - prevStockData.dateTime).TotalMinutes;

         if ( (int)dMinutes > interpDurationMinutes)
         {
            List<double> listInterpTicks = InterpolateValues(prevStockData.dateTime.Ticks, curStockData.dateTime.Ticks, dMinutes);
            List<double> listInterpPrice = InterpolateValues(prevStockData.price, curStockData.price, dMinutes);
            //List<double> listInterpVolume = InterpolateValues(prevStockData.volume, curStockData.volume, dMinutes);
            List<double> listInterpDailyHigh = InterpolateValues(prevStockData.dailyHigh, curStockData.dailyHigh, dMinutes);
            List<double> listInterpDailyLow = InterpolateValues(prevStockData.dailyLow, curStockData.dailyLow, dMinutes);

            for(int i = 0; i < listInterpPrice.Count; i++)
            {
               PriceStockType interpPriceData = new PriceStockType();
               interpPriceData.dateTime = new DateTime((long)listInterpTicks[i]);
               interpPriceData.price = listInterpPrice[i];
               interpPriceData.volume = prevStockData.volume; //listInterpVolume[i];
               interpPriceData.dailyHigh = listInterpDailyHigh[i]; //prevStockData.dailyHigh; 
               interpPriceData.dailyLow = listInterpDailyLow[i];  //prevStockData.dailyLow;

               listInterpData.Add(interpPriceData);
            }
         }

         return listInterpData;
      } // InterpolatePriceStockData()

      public static List<PriceCoinType> InterpolatePriceCoinData(PriceCoinType prevCoinData, PriceCoinType curCoinData, int interpDurationMinutes)
      {
         List<PriceCoinType> listInterpData = new List<PriceCoinType>();

         double dMinutes = (curCoinData.dateTime - prevCoinData.dateTime).TotalMinutes;

         if ((int)dMinutes > interpDurationMinutes)
         {
            List<double> listInterpTicks = InterpolateValues(prevCoinData.dateTime.Ticks, curCoinData.dateTime.Ticks, dMinutes);
            List<double> listInterpPrice = InterpolateValues(prevCoinData.price, curCoinData.price, dMinutes);
            //List<double> listInterpVolume = InterpolateValues(prevCoinData.volume, curCoinData.volume, dMinutes);
            //List<double> listInterpCirculatingSupply = InterpolateValues(prevCoinData.circulatingSupply, curCoinData.circulatingSupply, dMinutes);
            //List<double> listInterpMarketCap = InterpolateValues(prevCoinData.marketCap, curCoinData.marketCap, dMinutes);

            for (int i = 0; i < listInterpPrice.Count; i++)
            {
               PriceCoinType interpPriceData = new PriceCoinType();
               interpPriceData.dateTime = new DateTime((long)listInterpTicks[i]);
               interpPriceData.price = listInterpPrice[i];
               interpPriceData.volume = prevCoinData.volume; //listInterpVolume[i];
               interpPriceData.circulatingSupply = prevCoinData.circulatingSupply; //listInterpCirculatingSupply[i];
               interpPriceData.marketCap = prevCoinData.marketCap; //listInterpMarketCap[i];

               listInterpData.Add(interpPriceData);
            }
         }

         return listInterpData;
      } // InterpolatePriceCoinData()
   } // MathUtilities
}
