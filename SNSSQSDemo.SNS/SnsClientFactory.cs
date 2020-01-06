using Amazon.SimpleNotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace SNSSQSDemo.SNS
{
    static class SnsClientFactory
    {
        public static IAmazonSimpleNotificationService Create(SnsSettings settings)
        {
            var amazonSnsClient = new AmazonSimpleNotificationServiceClient(new AmazonSimpleNotificationServiceConfig()
            {
                ServiceURL = settings.ServiceURL
            });

            return amazonSnsClient;
        }
    }
}
