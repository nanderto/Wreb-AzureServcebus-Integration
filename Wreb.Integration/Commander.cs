// <copyright file="Commander.cs" company="Western Regional Examining Board">
// Copyright (c) 2020 Western Regional Examining Board. All rights reserved.
// </copyright>

namespace Wreb.Integration
{
    using Microsoft.Azure.ServiceBus;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="Commander" />.
    /// </summary>
    public class Commander
    {
        private readonly QueueClient queueClient;

        private readonly string queueName;

        private readonly string serviceBusConnectionString;

        private static IConfiguration config = null;

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
            var message = new Message(BinarySerializer.Serialize(command));
            //Message message = new Message(BinarySerializer.Serialize(command))
            //{
            //    // SessionId = sessionId
            //};

            await queueClient.SendAsync(message);
            return 0;
        }

        /// <summary>Sends the Command to Azure Servicebus asynchronously. 
        /// This command may be saved locally if the internet connection is down. 
        /// It will be persisted locally indefinitly until it is sucessfuly received by Azure Service Bus.
        /// </summary>
        /// <typeparam name="T">Of Type. Use the actual type of the command (not a base class)</typeparam>
        /// <param name="command">The command to send. </param>
        /// <returns>Either 0 to indicate a sucesful send, a negative number is a fault, or a positive number indicates which is the Id of the Command that has been sent</returns>
        public async Task<int> SendAsync<T>(ICommand command)
        {
            byte[] body = BinarySerializer.Serialize<T>(command);
            Message message = new Message(body);
            await queueClient.SendAsync(message);
            return 0;
        }
    }
}
