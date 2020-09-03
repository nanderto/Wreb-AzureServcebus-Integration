using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wreb.Integration;

namespace Wreb.Concrete
{
    public class MyCommandHandlerService : CommandHandlerService, ICommandHandlerService
    {
        public override void Initialize()
        {
             subscribers.Add(new ConsoleWriterSubscriber().WithYourCommandHandlerService(this).SubscribeToCommand(typeof(TestCommand)));
        }
    }
}
