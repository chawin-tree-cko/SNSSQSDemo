using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SNSSQSDemo.SQS
{
    public class SqsClient
    {
        private readonly IAmazonSQS client;
        private readonly SqsSettings settings;

        public SqsClient(IAmazonSQS client, SqsSettings settings)
        {
            this.client = client;
            this.settings = settings;
        }

        public async Task ReadMessageAsync(Func<Message, Task> messageProcess)
        {
            var receieveMessageRequest = new ReceiveMessageRequest
            {
                QueueUrl = settings.QueueUrl,
                MaxNumberOfMessages = settings.MaxMessages,
                VisibilityTimeout = settings.MessageTimeout
            };

            var response = await client.ReceiveMessageAsync(receieveMessageRequest);
            
            if (response.HttpStatusCode != HttpStatusCode.OK)
                throw new AmazonSQSException($"Invalid status code {response.HttpStatusCode} receieved");

            foreach (var message in response.Messages)
            {
                await messageProcess(message);

                await DeleteMessageAsync(message.ReceiptHandle);
            }
        }

        private async Task DeleteMessageAsync(string receiptHandle)
        {
            var deleteMessageRequest = new DeleteMessageRequest
            {
                QueueUrl = settings.QueueUrl,
                ReceiptHandle = receiptHandle
            };

            var response = await client.DeleteMessageAsync(deleteMessageRequest);

            if (response.HttpStatusCode != HttpStatusCode.OK)
                throw new AmazonSQSException($"Invalid status code {response.HttpStatusCode} receieved");
        }
    }
}
