using System;
using System.Threading.Tasks;

namespace Wreb.Integration
{
    public interface ISubscriber
    {
        Task ExecuteAsync(ICommand command);
        
        ISubscriber SubscribeToCommand(Type type);

        ISubscriber WithYourCommandHandlerService(ICommandHandlerService commandHandlerService);

        string CommandTypeSubscribedTo { get; set; }
    }
}