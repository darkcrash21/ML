using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockAnalyzer
{
   public class GraphDataPointType
   {
      public int index;
      public float value;
      public float x;         // Scaled 0.0 - 1.0
      public float y;         // Scaled 0.0 - 1.0

      public GraphDataPointType(int index, float value, float x, float y)
      {
         this.index = index;
         this.value = value;
         this.x = x;
         this.y = y;
      }

      public GraphDataPointType(int index, float value)
      {
         this.index = index;
         this.value = value;
         this.x = float.NaN;
         this.y = float.NaN;
      }
   } // GraphDataPointType

   public class GraphDataType
   {
      public string name;
      public Pen pen;
      private List<GraphDataPointType> listGraphPoints;
      private float min;
      private float max;
      private float bufferAmount = 0.01f;
      private float minBuffer;        // 10% offset lower
      private float maxBuffer;        // 10% offset lower

      public GraphDataType(string name, Pen pen)
      {
         this.name = name;
         this.pen = pen;
         this.listGraphPoints = new List<GraphDataPointType>();
         this.min = float.MaxValue;
         this.max = 0;
      }

      //
      // Public Setters
      //
      public void AddValue(float value)
      {
         if (value < this.min)
         {
            this.min = value;
         }
         if (value > this.max)
         {
            this.max = value;
         }

         GraphDataPointType point = new GraphDataPointType(this.listGraphPoints.Count, value);
         this.listGraphPoints.Add(point);
      }

      public void AddValue(double value)
      {
         this.AddValue((float)value);
      } // AddValue()

      public void AddValue(int value)
      {
         this.AddValue((float)value);
      } // AddValue()

      public void SetMinMaxBuffers()
      {
         // Scale the min and max offsets to add buffer around the min/max
         this.minBuffer = this.min * (1 - bufferAmount);
         this.maxBuffer = this.max * (1 + bufferAmount);
      } // SetMinMaxBuffers()

      public void ScaleGraphPoints()
      {
         foreach (GraphDataPointType point in this.listGraphPoints)
         {
            point.x = (float)point.index / (float)this.listGraphPoints.Count; ;
            point.y = (point.value - this.minBuffer) / (this.maxBuffer - this.minBuffer);
         }
      } // ScaleGraphPoints()

      public void ScaleGraphPoints(float minValue, float maxValue)
      {
         foreach (GraphDataPointType point in this.listGraphPoints)
         {
            point.x = (float)point.index / (float)this.listGraphPoints.Count; ;
            point.y = (point.value - minValue) / (maxValue - minValue);
         }
      } // ScaleGraphPoints()

      //
      // Public Getters
      //
      public float Min()
      {
         return this.min;
      } // Min()

      public float Max()
      {
         return this.max;
      } // Max()

      public float MinBuffer()
      {
         return this.minBuffer;
      } // MinBuffer()

      public float MaxBuffer()
      {
         return this.maxBuffer;
      } // MaxBuffer()

      public List<GraphDataPointType> GetPoints()
      {
         GraphDataPointType[] newList = new GraphDataPointType[this.listGraphPoints.Count];
         this.listGraphPoints.CopyTo(newList);
         return newList.ToList();
      } // GetPoints()

      public int Count()
      {
         return this.listGraphPoints.Count;
      } // Count()
   } // GraphDataTypes

   public class GdiDataPointType
   {
      public PointF point;
      public GraphDataPointType data;
   } // GdiDataPointType

   public class GdiDataType
   {
      public string name;
      public Pen pen;
      public List<GdiDataPointType> dataPoints = new List<GdiDataPointType>();
   } // GdiDataType()
}
