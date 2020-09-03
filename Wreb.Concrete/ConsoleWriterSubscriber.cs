using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wreb.Integration;

namespace Wreb.Concrete
{
    public class ConsoleWriterSubscriber : SubscriberBase, ISubscriber
    {
        public override async Task ExecuteAsync(ICommand command)
        {
            Console.WriteLine($"Executing subscriber ConsoleWriterSubscriber, the ClientId of the command is {command.ClientId} ");
        }
    }

    public class ConsoleWriterSubscriber2 : SubscriberBase, ISubscriber
    {
        public override async Task ExecuteAsync(ICommand command)
        {
            Console.WriteLine($"Executing subscriber ConsoleWriterSubscriber2, the ClientId of the command is {command.ClientId} ");
        }
    }
}
