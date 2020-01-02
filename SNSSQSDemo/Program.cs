using Amazon.SimpleNotificationService;
using Amazon.SQS;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using SNSSQSDemo.SNS;
using SNSSQSDemo.SQS;

namespace SNSSQSDemo
{
    class Program
    {
        private const string snsUrl = "http://localhost:4575";
        private const string snsTopicArn = "arn:aws:sns:us-east-1:000000000000:topic-name";
        
        private const string sqsUrl = "http://localhost:4576";
        private const string sqsQueueUrl = "http://localhost:4576/queue/queue-name";
        private const int sqsMaxMessages = 1;
        private const int sqsMessageTimeout = 10;

        private const int messagesToPublish = 10;

        static SqsClient sqsClient;
        static SnsClient snsClient;

        static void Main()
        {
            Setup();

            _ = PublishMessages();
            _ = ReadMessages();

            Console.ReadKey();
        }

        static async Task PublishMessages()
        {
            for (int i = 1; i <= messagesToPublish; i++)
            {
                Console.WriteLine($"Published {i}");

                await snsClient.PublishMessageAsync($"Received {i}");
            }
        }

        static async Task ReadMessages()
        {
            while (true)
            {
                await sqsClient.ReadMessageAsync(async x => 
                { 
                    var message = JsonSerializer.Deserialize<SqsMessage>(x.Body);
                    Console.WriteLine(message.Message);
                    await Task.CompletedTask;
                });
            }
        }

        static void Setup()
        {
            // SNS
            var amazonSnsConfig = new AmazonSimpleNotificationServiceConfig()
            {
                ServiceURL = snsUrl
            };
            var amazonSnsClient = new AmazonSimpleNotificationServiceClient(amazonSnsConfig);

            var snsSettings = new SnsSettings()
            {
                ServiceURL = snsUrl,
                TopicArn = snsTopicArn
            };

            snsClient = new SnsClient(amazonSnsClient, snsSettings);

            // SQS

            var amazonSqsConfig = new AmazonSQSConfig()
            {
                ServiceURL = sqsUrl
            };
            var amazonSqsClient = new AmazonSQSClient(amazonSqsConfig);

            var sqsSettings = new SqsSettings()
            {
                MaxMessages = sqsMaxMessages,
                MessageTimeout =sqsMessageTimeout, 
                QueueUrl = sqsQueueUrl
            };

            sqsClient = new SqsClient(amazonSqsClient, sqsSettings);
        }
    }
}
