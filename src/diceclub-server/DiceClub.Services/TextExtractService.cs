using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Textract;
using Amazon.Textract.Model;
using Aurora.Api.Interfaces.Services;
using Aurora.Api.Services.Base;
using DiceClub.Api.Data.Cards;
using DiceClub.Api.Data.Credentials;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DiceClub.Services
{
    public class TextExtractService : AbstractBaseService<TextExtractService>, INotificationHandler<ImageCardCreatedEvent>
    {
        private readonly AwsCredentialsObject _credentials;
        private readonly string _s3BucketName = "mtg-image-repository";
        public TextExtractService(IEventBusService eventBusService, ILogger<TextExtractService> logger, AwsCredentialsObject credentials) : base(eventBusService, logger)
        {
            _credentials = credentials;
        }

        public Task Handle(ImageCardCreatedEvent notification, CancellationToken cancellationToken)
        {

            Task.Run(() => StartTextExtractJob(notification.FileName), cancellationToken);

            return Task.CompletedTask;
        }

        private async Task StartTextExtractJob(string fileName)
        {
            Logger.LogInformation("Starting extract text for file: {File}", fileName);
            using var s3Service = new AmazonS3Client(new BasicAWSCredentials(_credentials.AwsAccessKey, _credentials.AwsSecretKey), RegionEndpoint.GetBySystemName(_credentials.Region));
            using var textractClient = new AmazonTextractClient(
                new BasicAWSCredentials(_credentials.AwsAccessKey, _credentials.AwsSecretKey),
                RegionEndpoint.GetBySystemName(_credentials.Region));

            var putRequest = new PutObjectRequest
            {
                BucketName = _s3BucketName,
                FilePath = fileName,
                Key = Path.GetFileName(fileName)
            };

            await s3Service.PutObjectAsync(putRequest);

            var startResponse = await textractClient.StartDocumentAnalysisAsync(new StartDocumentAnalysisRequest
            {

                DocumentLocation = new DocumentLocation
                {
                    S3Object = new Amazon.Textract.Model.S3Object
                    {
                        Bucket = _s3BucketName,
                        Name = putRequest.Key
                    }
                },
                FeatureTypes = new List<string>() { "FORMS" }

            });

            var getDetectionRequest = new GetDocumentAnalysisRequest
            {
                JobId = startResponse.JobId
            };

            // Poll till job is no longer in progress.
            GetDocumentAnalysisResponse getDetectionResponse = null;
            List<Block> blocks = new();
            do
            {
                await Task.Delay(500);

                getDetectionResponse = await textractClient.GetDocumentAnalysisAsync(getDetectionRequest);
            } while (getDetectionResponse.JobStatus == JobStatus.IN_PROGRESS);

            if (getDetectionResponse.JobStatus == JobStatus.SUCCEEDED)
            {
                do
                {
                    blocks.AddRange(getDetectionResponse.Blocks);

                    // Check to see if there are no more pages of data. If no then break.
                    if (string.IsNullOrEmpty(getDetectionResponse.NextToken))
                    {
                        break;
                    }

                    getDetectionRequest.NextToken = getDetectionResponse.NextToken;
                    getDetectionResponse = await textractClient.GetDocumentAnalysisAsync(getDetectionRequest);

                } while (!string.IsNullOrEmpty(getDetectionResponse.NextToken));
                //getDetectionResponse.Blocks =
                //    getDetectionResponse.Blocks.Where(s => s.BlockType == BlockType.LINE).ToList();

                //foreach (var block in blocks)
                //{
                //    Logger.LogInformation("Card: {Type} - {Text}", block.BlockType, block.Text);
                //}
                //  var text = string.Join(";", getDetectionResponse.Blocks.Select(s => s.Text).ToList());

                var textBlock = blocks.FirstOrDefault(s => s.BlockType == BlockType.LINE);
                Logger.LogInformation("Card: {Text}", textBlock.Text);

            }
            else
            {
                Console.WriteLine($"Job failed with message: {getDetectionResponse.StatusMessage}");
            }
        }
    }
}
