using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Wreb.Concrete;
using Wreb.Integration;
using Wreb.Integration.Tests;

namespace Wreb.Servicebus
{
    public class CommandReceiver
    {
        private readonly ICommandHandlerService commandHandlerService;

        public CommandReceiver(ICommandHandlerService commandHandlerService)
        {
            this.commandHandlerService = commandHandlerService;
        }

        [FunctionName("CommandReceiver")]
        public async Task RunAsync([ServiceBusTrigger("firstqueue", Connection = "AzureServicebusConnection")] Message myQueueItem, ILogger log)
        {

            ICommand command = BinarySerializer.Deserialize<ICommand>(myQueueItem.Body, this.commandHandlerService.KnownTypes);

           // Console.WriteLine($"Received message: ClientId:{deSerializedMessage.ClientId} OriginUser:{deSerializedMessage.OriginUser}");
            //Console.WriteLine($"Received message: SequenceNumber:{myQueueItem} Body:{deSerializedMessage.GetType().FullName}");
            
            //C0onsole.WriteLine($"TestCommand: ClientId:{command.ClientId.ToString()} OriginUser:{command.OriginUser}");

            var commandHandler = new CommandHandler();

            await this.commandHandlerService.HandleCommandAsync(command);
        }
    }

}
