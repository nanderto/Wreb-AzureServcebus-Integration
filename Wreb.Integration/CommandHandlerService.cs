using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wreb.Integration;

namespace Wreb.Concrete
{
    public class CommandHandlerService : ICommandHandlerService
    {
        protected List<ISubscriber> subscribers = new List<ISubscriber>();

        public List<Type> KnownTypes { get; set; } = new List<Type>();

        public CommandHandlerService()
        {
            Initialize();
        }
        public virtual void Initialize()
        {
             
        }

        public async Task HandleCommandAsync(ICommand command)
        {
            Console.WriteLine("here");
            foreach (var item in subscribers)
            {
                await item.ExecuteAsync(command);
            }
        }
    }
}
