using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Wreb.Servicebus
{
    public static class CommandReceiver
    {
        [FunctionName("CommandReceiver")]
        public static void Run([ServiceBusTrigger("firstqueue", Connection = "AzureServicebusConnection")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
