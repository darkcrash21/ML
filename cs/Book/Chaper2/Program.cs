using Chapter2.ML;
using System;

namespace Chapter2
{
   class Program
   {
      static void Main(string[] args)
      {
         if (args.Length != 2)
         {
            Console.WriteLine("bad call!");
            return;
         }

         switch (args[0])
         {
            case "predict":
               new Predictor().Predict(args[1]);
               break;
            case "train":
               new Trainer().Train(args[1]);
               break;
            default:
               Console.WriteLine("bad args");
               break;
         }

      }
   }
}
