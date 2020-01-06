using Amazon.SQS;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace SNSSQSDemo.SQS
{
    class Program
    {
        private const string sqsUrl = "http://localhost:4576";
        private const string sqsQueueUrl = "http://localhost:4576/queue/queue-name";
        private const int sqsMaxMessages = 1;
        private const int sqsMessageTimeout = 10;

        static SqsClient client;

        static async Task Main()
        {
            Setup();

            Console.WriteLine("SQS Message Reader");
            Console.WriteLine($"Reading messages from {sqsQueueUrl}");

            while (true)
            {
                await client.ReadMessageAsync(async x => 
                { 
                    var message = JsonSerializer.Deserialize<SqsMessage>(x.Body);
                    Console.WriteLine(message.Message);
                    await Task.CompletedTask;
                });
            }
        }

        static void Setup()
        {
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

            client = new SqsClient(amazonSqsClient, sqsSettings);
        }
    }
}
