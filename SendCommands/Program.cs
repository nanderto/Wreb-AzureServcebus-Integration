using Microsoft.Azure.ServiceBus;
using System;
using System.Threading.Tasks;
using Wreb.Integration;
using Wreb.Integration.Tests;

namespace SendCommands
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const int numberOfMessages = 10;

            Console.WriteLine("======================================================");
            Console.WriteLine("Press ENTER key to exit after sending all the messages.");
            Console.WriteLine("======================================================");

            // Send messages.
            await SendMessagesAsync(numberOfMessages);

            Console.ReadKey();
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
                    var message = new Message(BinarySerializer.Serialize(command));

                    // Write the body of the message to the console.
                    Console.WriteLine($"Sending message: {command.ToString()}");

                    // Send the message to the queue.
                    await commander.SendAsync(command);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
    }
}
