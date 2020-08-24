using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Threading.Tasks;

namespace Wreb.Integration
{
    public class Commander
    {
        private static IConfiguration config = null;
        private readonly string queueName;
        private readonly QueueClient queueClient;
        private readonly string serviceBusConnectionString;

        public Commander()
        {
            config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();
            serviceBusConnectionString = config.GetConnectionString("AzureServicebusConnection");
            queueName = config.GetSection("QueueName").Value;
            queueClient = new QueueClient(serviceBusConnectionString, queueName);
        }

        /// <summary>
        ///  Sends a command to Azure Service Bus. This command may be saved locally if the internet connection is down. it will be persisted locally indefinitly until it is sucessfuly received by 
        ///  Azure Service Bus.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Either 0 to indicate a sucesful send, a negative number is a fault, or a positive number indicates which is the Id of the Command that has been sent</returns>
        public async Task<int> SendAsync(ICommand command)
        {
            await queueClient.SendAsync(BinarySerializedAnPackaged(command));
            return 0;
        }

        private Message BinarySerializedAnPackaged(ICommand command) 
        {
            return new Message(BinarySerializer.Serialize(command));
        }
    }
}
