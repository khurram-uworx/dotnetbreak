using Microsoft.ML;
using Microsoft.ML.TorchSharp.NasBert;
using Microsoft.ML.TorchSharp;
using Microsoft.ML.Data;

namespace ML.Classifier;
//https://github.com/IntegerMan/MattEland.AI.MLNet.TextClassificationTurtles/blob/main/MattEland.AI.MLNet.TextClassificationTurtles/Program.cs
//https://accessibleai.dev/post/ml_net_2_0_text_classification/

internal class Program
{
    static void Main(string[] args)
    {
        MLContext mlContext = new()
        {
            GpuDeviceId = 0,
            FallbackToCpu = true
        };

        // Load the data source
        Console.WriteLine("Loading data...");
        IDataView dataView = mlContext.Data.LoadFromTextFile<ModelInput>(
            "data.tsv",
            separatorChar: '\t',
            hasHeader: false
        );

        DataOperationsCatalog.TrainTestData dataSplit = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2, seed: 1234);
        IDataView trainData = dataSplit.TrainSet;
        IDataView testData = dataSplit.TestSet;

        // Create a pipeline for training the model
        var pipeline = mlContext.Transforms.Conversion.MapValueToKey(
            outputColumnName: "Label",
            inputColumnName: "Label")
            .Append(mlContext.MulticlassClassification.Trainers.TextClassification(
                labelColumnName: "Label",
                sentence1ColumnName: "Sentence",
                architecture: BertArchitecture.Roberta))
            .Append(mlContext.Transforms.Conversion.MapKeyToValue(
                outputColumnName: "PredictedLabel",
                inputColumnName: "PredictedLabel"));

        Console.WriteLine("Training model...");
        ITransformer model = pipeline.Fit(trainData);

        Console.WriteLine("Evaluating model performance...");

        IDataView transformedTest = model.Transform(testData);
        MulticlassClassificationMetrics metrics = mlContext.MulticlassClassification.Evaluate(transformedTest);

        Console.WriteLine($"Macro Accuracy: {metrics.MacroAccuracy}");
        Console.WriteLine($"Micro Accuracy: {metrics.MicroAccuracy}");
        Console.WriteLine($"Log Loss: {metrics.LogLoss}");
        Console.WriteLine();

        Console.WriteLine("Classes:");
        foreach (Intents value in Enum.GetValues<Intents>())
            Console.WriteLine($"{((int)value)}: {value}");
        Console.WriteLine();

        Console.WriteLine(metrics.ConfusionMatrix.GetFormattedConfusionTable());

        Console.WriteLine("Creating prediction engine...");
        PredictionEngine<ModelInput, ModelOutput> engine =
            mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model);

        Console.WriteLine("Ready to generate predictions.");

        string input;
        do
        {
            Console.WriteLine();
            Console.WriteLine("What do you want to say? (Type Q to Quit)");
            input = Console.ReadLine()!;

            // Get a prediction
            ModelInput sampleData = new(input);
            ModelOutput result = engine.Predict(sampleData);

            // Print classification
            float maxScore = result.Score[(uint)result.PredictedLabel];
            Console.WriteLine($"Matched intent {(Intents)result.PredictedLabel} with score of {maxScore:f2}");
            Console.WriteLine();
        }
        while (!string.IsNullOrWhiteSpace(input) && input.ToLowerInvariant() != "q");
    }
}
