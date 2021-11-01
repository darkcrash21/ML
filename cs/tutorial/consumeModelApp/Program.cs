using System;
using SampleClassification.Model;

namespace consumeModelApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Add input data
            var input = new ModelInput()
            {
              Col0 = "The food was so disgusting that i couldn't even finish it."
            };

            // Load model and predict output of sample data
            ModelOutput result = ConsumeModel.Predict(input);
            
            // If Prediction is 1, sentiment is "Positive"; otherwise, sentiment is "Negative"
            string sentiment = result.Prediction == "1" ? "Positive" : "Negative";

            Console.WriteLine($"Text: {input.Col0}\nGuess: {sentiment}\nScores:");

            foreach(float score in result.Score)
            {
                Console.WriteLine($"{score}");
            }
        }
    }
}