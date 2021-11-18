using System;
using System.IO;

using stockPredictor.ML.Base;
using stockPredictor.ML.Objects;

using Microsoft.ML;

using Newtonsoft.Json;

namespace stockPredictor.ML
{
   public class Predictor : BaseML
   {
      public void Predict(string inputDataFile)
      {
         if (!File.Exists(ModelPath))
         {
            Console.WriteLine($"Failed to find model at {ModelPath}");

            return;
         }

         if (!File.Exists(inputDataFile))
         {
            Console.WriteLine($"Failed to find input data at {inputDataFile}");

            return;
         }

         ITransformer mlModel;

         using (var stream = new FileStream(ModelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
         {
            mlModel = MlContext.Model.Load(stream, out _);
         }

         if (mlModel == null)
         {
            Console.WriteLine("Failed to load model");

            return;
         }

         var predictionEngine = MlContext.Model.CreatePredictionEngine<StockPriceHistory, StockPricePrediction>(mlModel);

         var json = File.ReadAllText(inputDataFile);

         var prediction = predictionEngine.Predict(JsonConvert.DeserializeObject<StockPriceHistory>(json));

         Console.WriteLine(
                             $"Based on input json:{System.Environment.NewLine}" +
                             $"{json}{System.Environment.NewLine}" +
                             $"The employee is predicted to work {prediction.Price:#.##} Price");
      }
   }
}