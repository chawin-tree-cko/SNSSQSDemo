using Amazon.SimpleNotificationService;
using System;

namespace SNSSQSDemo.SNS
{
    class Program
    {
        private const string snsUrl = "http://localhost:4575";
        private const string snsTopicArn = "arn:aws:sns:us-east-1:000000000000:topic-name";
        
        static SnsClient client;

        static void Main()
        {   
            Setup();

            Console.WriteLine("SNS Message Publisher");
            Console.WriteLine("Enter a message to send to SNS, e.g. 'Hello world'");
            Console.WriteLine("Alternatively, enter a number to send a batch of messagse of that amount, e.g. 10");
            Console.WriteLine("Enter 'q' to exit");

            var entry = Console.ReadLine();

            while (!string.IsNullOrEmpty(entry) && entry != "q")
            {
                if (int.TryParse(entry, out var x))
                {
                    Console.WriteLine($"Sending {x} messages");

                    for (int i = 1; i <= x; i++)
                    {
                        _ = client.PublishMessageAsync($"Message number {i}");
                    }
                }
                else
                {
                    _ = client.PublishMessageAsync(entry);
                }

                entry = Console.ReadLine();
            }
        }

        static void Setup()
        {
            var amazonSnsConfig = new AmazonSimpleNotificationServiceConfig()
            {
                ServiceURL = snsUrl
            };
            var amazonSnsClient = new AmazonSimpleNotificationServiceClient(amazonSnsConfig);

            var settings = new SnsSettings()
            {
                ServiceURL = snsUrl,
                TopicArn = snsTopicArn
            };

            client = new SnsClient(amazonSnsClient, settings);
        }
    }
}
