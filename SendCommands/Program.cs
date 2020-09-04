using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wreb.Concrete;
using Wreb.Integration;
using Wreb.Integration.Tests;

namespace SendCommands
{
    class Program
    {
        private static bool UseBinaryFormater = false;

        static async Task Main(string[] args)
        {
            int result = 10;
            int numberOfMessages = 10;
            
            if ((args.Length > 0) && int.TryParse(args[0], out result))
            {
                numberOfMessages = result;
            }

            if (args.Length > 1) 
            {
                if (args[1].Substring(0,1) == "-")
                {
                    await SendSecondMessagesAsync(numberOfMessages);
                    return;
                }
                else
                {
                    if ((args.Length > 1) && int.TryParse(args[0], out result))
                    {
                        UseBinaryFormater = true;
                    }
                }
            }

            // Send messages.
            await SendMessagesAsync(numberOfMessages);
        }

        static async Task SendMessagesAsync(int numberOfMessagesToSend)
        {
            try
            {
                var commander = new Commander();

                for (var i = 0; i < numberOfMessagesToSend; i++)
                {
                    // Create a new message to send to the queue.
                    var command = new TestCommand("originUser", "SystemX", typeof(Command).FullName, Guid.NewGuid().ToString(), null, Guid.NewGuid().ToString());

                    // Write the body of the message to the console.
                    Console.WriteLine($"Sending message with ClientId: {command.ClientId}");

                    // Send the message to the queue.
                    if (UseBinaryFormater)
                    {
                        await commander.SendAsync(command);
                    }
                    else
                    {
                        await commander.SendAsync<ICommand>(command);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }

        static async Task SendSecondMessagesAsync(int numberOfMessagesToSend)
        {
            try
            {
                var commander = new Commander();

                for (var i = 0; i < numberOfMessagesToSend; i++)
                {
                    // Create a new message to send to the queue.
                    var command = new TestCommand2("originUser", "SystemX", typeof(Command).FullName, Guid.NewGuid().ToString(), null, Guid.NewGuid().ToString(), "This is the Test Property 2");

                    // Write the body of the message to the console.
                    Console.WriteLine($"Sending message with ClientId: {command.ClientId} :: {command.TestProperty2}");

                    // Send the message to the queue.
                    if (UseBinaryFormater)
                    {
                        await commander.SendAsync(command);
                    }
                    else
                    {
                        await commander.SendAsync<ICommand>(command);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }

    }
}
