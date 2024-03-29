﻿using System;
using System.IO;

using Chapter2.Common;

using Microsoft.ML;

namespace Chapter2.ML.Base
{
   public class BaseML
   {
      protected static string ModelPath => Path.Combine(AppContext.BaseDirectory, Constants.MODEL_FILENAME);

      protected readonly MLContext MlContext;

      protected BaseML()
      {
         MlContext = new MLContext(2020);
      }
   }
}