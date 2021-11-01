using System;
using System.IO;

using Chapter2.ML.Base;
using Chapter2.ML.Objects;

using Microsoft.ML;

namespace Chapter2.ML
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

         IDataView trainingDataView = MlContext.Data.LoadFromTextFile<RestaurantFeedback>(trainingFileName);

         DataOperationsCatalog.TrainTestData dataSplit = MlContext.Data.TrainTestSplit(trainingDataView, testFraction: 0.2);

         Microsoft.ML.Transforms.Text.TextFeaturizingEstimator dataProcessPipeline = MlContext.Transforms.Text.FeaturizeText(
             outputColumnName: "Features",
             inputColumnName: nameof(RestaurantFeedback.Text));

         Microsoft.ML.Trainers.SdcaLogisticRegressionBinaryTrainer sdcaRegressionTrainer = MlContext.BinaryClassification.Trainers.SdcaLogisticRegression(
             labelColumnName: nameof(RestaurantFeedback.Label),
             featureColumnName: "Features");

         var trainingPipeline = dataProcessPipeline.Append(sdcaRegressionTrainer);

         ITransformer trainedModel = trainingPipeline.Fit(dataSplit.TrainSet);
         MlContext.Model.Save(trainedModel, dataSplit.TrainSet.Schema, ModelPath);

         var testSetTransform = trainedModel.Transform(dataSplit.TestSet);

         Microsoft.ML.Data.CalibratedBinaryClassificationMetrics modelMetrics = MlContext.BinaryClassification.Evaluate(
             data: testSetTransform,
             labelColumnName: nameof(RestaurantFeedback.Label),
             scoreColumnName: nameof(RestaurantPrediction.Score));

         Console.WriteLine($"Area Under Curve: {modelMetrics.AreaUnderRocCurve:P2}{Environment.NewLine}" +
                           $"Area Under Precision Recall Curve: {modelMetrics.AreaUnderPrecisionRecallCurve:P2}{Environment.NewLine}" +
                           $"Accuracy: {modelMetrics.Accuracy:P2}{Environment.NewLine}" +
                           $"F1Score: {modelMetrics.F1Score:P2}{Environment.NewLine}" +
                           $"Positive Recall: {modelMetrics.PositiveRecall:#.##}{Environment.NewLine}" +
                           $"Negative Recall: {modelMetrics.NegativeRecall:#.##}{Environment.NewLine}");
      }
   }
}