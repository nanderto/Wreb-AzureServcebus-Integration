using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wreb.Integration;

namespace Wreb.Concrete
{
    public class CommandHandlerService : ICommandHandlerService
    {
        protected Dictionary<string, List<ISubscriber>> SubscribersForEachCommand = new Dictionary<string, List<ISubscriber>>();

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
            var subscribers = SubscribersForEachCommand[command.GetType().FullName];
            foreach (var item in subscribers)
            {
                await item.ExecuteAsync(command);
            }
        }

        protected void Add(ISubscriber configuredSubscriber)
        {
            var commandTypeSubscribedTo = configuredSubscriber.CommandTypeSubscribedTo;
            if (SubscribersForEachCommand.ContainsKey(commandTypeSubscribedTo))
            {
                SubscribersForEachCommand[commandTypeSubscribedTo].Add(configuredSubscriber);
            }
            else
            {
                var newSubscriberList = new List<ISubscriber> { configuredSubscriber };
                SubscribersForEachCommand.Add(commandTypeSubscribedTo, newSubscriberList);
            }
        }

        /// <summary>
        /// Tries the get subscriber list. This method was added for unit testnig. please remove this comment
        /// if this method is used as part of the application
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="subscriberList">The subscriber list.</param>
        /// <returns>true if subscriber list is found</returns>
        public bool TryGetSubscriberList (string key, out List<ISubscriber> subscriberList)
        {
            return SubscribersForEachCommand.TryGetValue(key, out subscriberList);
        }
    }
}
