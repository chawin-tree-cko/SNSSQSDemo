namespace SNSSQSDemo.SQS
{
    public class SqsSettings
    {
        /// <summary>
        /// URL of the queue to read messages.
        /// In the format "https://sqs.{REGION}.amazonaws.com/{ACCOUNT_ID}/{QUEUE_NAME}".
        /// </summary>
        public string QueueUrl { get; set; }

        /// <summary>
        /// Number of messages to retrieve per read, with a maximum of 10.
        /// </summary>
        public int MaxMessages { get; set; }

        /// <summary>
        /// The time, in seconds, to lock the message for processing. 
        /// If not deleted using the message's ReceiptHandle, it can be read again.
        /// </summary>
        public int MessageTimeout { get; set; }
    }
}
