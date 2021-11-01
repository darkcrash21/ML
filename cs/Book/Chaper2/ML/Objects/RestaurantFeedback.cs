using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chapter2.ML.Objects
{
   public class RestaurantFeedback
   {
      [LoadColumn(0)]
      public bool Label { get; set; }

      [LoadColumn(1)]
      public string Text { get; set; }
   }
}
