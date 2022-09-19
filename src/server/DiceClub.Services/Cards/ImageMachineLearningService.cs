using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace DiceClub.Services.Cards;

public class ImageMachineLearningService : AbstractBaseService<ImageMachineLearningService>
{
    public ImageMachineLearningService(IEventBusService eventBusService, ILogger<ImageMachineLearningService> logger) :
        base(eventBusService, logger)
    {
    }

    public Task TrainModel(string assetsPath, string dataPath, string tagFileName)
    {
        var inceptionTensorFlowModel = Path.Combine(assetsPath, "tensorflow_inception_graph.pb");
        var imagesFolder = Path.Combine(dataPath, "images");
        var trainTagsTsv = Path.Combine(dataPath, tagFileName);

        var mlContext = new MLContext();
        var pipeline = mlContext.Transforms.LoadImages(outputColumnName: "input",
                imageFolder: imagesFolder, inputColumnName: nameof(ImageData.ImagePath))
            // The image transforms transform the images into the model's expected format.
            .Append(mlContext.Transforms.ResizeImages(outputColumnName: "input",
                imageWidth: InceptionSettings.ImageWidth, imageHeight: InceptionSettings.ImageHeight,
                inputColumnName: "input"))
            .Append(mlContext.Transforms.ExtractPixels(outputColumnName: "input",
                interleavePixelColors: InceptionSettings.ChannelsLast, offsetImage: InceptionSettings.Mean))
            .Append(mlContext.Model.LoadTensorFlowModel(inceptionTensorFlowModel).ScoreTensorFlowModel(
                outputColumnNames: new[] { "softmax2_pre_activation" }, inputColumnNames: new[] { "input" },
                addBatchDimensionInput: true))
            .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "LabelKey",
                inputColumnName: "Label"))
            .Append(mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy(labelColumnName: "LabelKey",
                featureColumnName: "softmax2_pre_activation"))
            .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabelValue", "PredictedLabel"))
            .AppendCacheCheckpoint(mlContext);

        var trainingData = mlContext.Data.LoadFromTextFile<ImageData>(path: trainTagsTsv, hasHeader: false);

        var model = pipeline.Fit(trainingData);
        mlContext.Model.Save(model, trainingData.Schema, Path.Join(assetsPath, "model.zip"));

        return Task.CompletedTask;
    }

    struct InceptionSettings
    {
        public const int ImageHeight = 321;
        public const int ImageWidth = 223;
        public const float Mean = 117;
        public const float Scale = 1;
        public const bool ChannelsLast = true;
    }

    public class ImageData
    {
        [LoadColumn(0)] public string ImagePath;

        [LoadColumn(1)] public string Label;
    }

    public class ImagePrediction : ImageData
    {
        public float[] Score;

        public string PredictedLabelValue;
    }
}