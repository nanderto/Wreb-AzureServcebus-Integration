using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Wreb.Integration
{
    public abstract class SubscriberBase : ISubscriber
    {
        public ICommandHandlerService CommandHandlerService;

        public ICommand Command;

        public string CommandTypeSubscribedTo { get; set; }


        public ISubscriber SubscribeToCommand(Type type)
        {
            this.CommandHandlerService.KnownTypes.Add(type);
            CommandTypeSubscribedTo = type.FullName;
            return this;
        }

        public abstract Task ExecuteAsync(ICommand command);


        public ISubscriber WithYourCommandHandlerService(ICommandHandlerService commandHandlerService)
        {
            this.CommandHandlerService = commandHandlerService;
            return this;
        }
    }
}
