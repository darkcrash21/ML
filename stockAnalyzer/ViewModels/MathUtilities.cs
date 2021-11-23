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
    } // MathUtilities
}
