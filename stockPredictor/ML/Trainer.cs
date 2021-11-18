using System;
using System.IO;

using stockPredictor.Common;
using stockPredictor.ML.Base;
using stockPredictor.ML.Objects;

using Microsoft.ML;

namespace stockPredictor.ML
{
   public class Trainer : BaseML
   {
      public void Train(string trainingFileName)
      {
         if (!File.Exists(trainingFileName))
         {
            Console.WriteLine($"Failed to find training data file ({trainingFileName}");

            return;
         }
         
         var trainingDataView = MlContext.Data.LoadFromTextFile<StockPriceHistory>(trainingFileName, ',', true, false, true, false);

         var dataSplit = MlContext.Data.TrainTestSplit(trainingDataView, testFraction: 0.4);

         var dataProcessPipeline = MlContext.Transforms.CopyColumns("Label", nameof(StockPriceHistory.Price))
             .Append(MlContext.Transforms.NormalizeMeanVariance(nameof(StockPriceHistory.DateTime)))
             .Append(MlContext.Transforms.NormalizeMeanVariance(nameof(StockPriceHistory.MarketCap)))
             .Append(MlContext.Transforms.NormalizeMeanVariance(nameof(StockPriceHistory.Volume)))
             //.Append(MlContext.Transforms.NormalizeMeanVariance(nameof(StockPriceHistory.CirculatingSupply)))
             .Append(MlContext.Transforms.Concatenate("Features",
                 typeof(StockPriceHistory).ToPropertyList<StockPriceHistory>(nameof(StockPriceHistory.Price))));

         var trainer = MlContext.Regression.Trainers.Sdca(labelColumnName: "Label", featureColumnName: "Features");

         var trainingPipeline = dataProcessPipeline.Append(trainer);

         ITransformer trainedModel = trainingPipeline.Fit(dataSplit.TrainSet);
         MlContext.Model.Save(trainedModel, dataSplit.TrainSet.Schema, ModelPath);

         var testSetTransform = trainedModel.Transform(dataSplit.TestSet);

         var modelMetrics = MlContext.Regression.Evaluate(testSetTransform);

         Console.WriteLine($"Loss Function: {modelMetrics.LossFunction:0.##}{Environment.NewLine}" +
                           $"Mean Absolute Error: {modelMetrics.MeanAbsoluteError:#.##}{Environment.NewLine}" +
                           $"Mean Squared Error: {modelMetrics.MeanSquaredError:#.##}{Environment.NewLine}" +
                           $"RSquared: {modelMetrics.RSquared:0.##}{Environment.NewLine}" +
                           $"Root Mean Squared Error: {modelMetrics.RootMeanSquaredError:#.##}");
      }
   }
}