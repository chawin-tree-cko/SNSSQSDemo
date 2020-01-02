using Amazon.SimpleNotificationService;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SNSSQSDemo.SNS
{
    public class SnsClient
    {
        private readonly IAmazonSimpleNotificationService client;
        private readonly SnsSettings settings;

        public SnsClient(IAmazonSimpleNotificationService client, SnsSettings settings)
        {
            this.client = client;
            this.settings = settings;
        }

        public async Task PublishMessageAsync(string message)
        {
            var response = await client.PublishAsync(settings.TopicArn, message);

            if (response.HttpStatusCode != HttpStatusCode.OK)
                throw new Exception();
        }
    }
}
