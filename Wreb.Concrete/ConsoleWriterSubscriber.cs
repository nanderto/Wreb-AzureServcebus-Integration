using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wreb.Integration;

namespace Wreb.Concrete
{
    public class ConsoleWriterSubscriber : SubscriberBase, ISubscriber
    {
 


        //public ConsoleWriterSubscriber(CommandHandlerService commandHandlerService)
        //{
        //    this.CommandHandlerService = commandHandlerService;
        //}

        public override async Task ExecuteAsync(ICommand command)
        {
            Console.WriteLine($"Executing subscriber ConsoleWriterSubscriber, the UniqueKey of the command is {command.UniqueKey} ");
        }
    }
}
