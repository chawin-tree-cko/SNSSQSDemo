namespace SNSSQSDemo.SNS
{
    public class SnsSettings
    {
        /// <summary>
        /// ARN of the topic to publish messages to.
        /// In the format "arn:aws:sns:{REGION}:{ACCOUNT_ID}:{TOPIC_NAME}"
        /// </summary>
        public string TopicArn { get; set; }

        /// <summary>
        /// URL for SNS (only required for LocalStack)
        /// </summary>
        public string ServiceURL { get; internal set; }
    }
}
