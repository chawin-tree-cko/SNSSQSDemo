using System;

namespace SNSSQSDemo.SQS
{
    public class SqsMessage
    {
        public string MessageId { get; set; }
        public string Type { get; set; }
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
        public string TopicArn { get; set; }
    }
}
