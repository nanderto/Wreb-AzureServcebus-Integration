using System;
using Wreb.Integration;

namespace ReceiveCommands
{
    class Program
    {
        private static Receiver receiver;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            receiver = new Receiver();

            Console.WriteLine("======================================================");
            Console.WriteLine("Press ENTER key to exit after receiving all the messages.");
            Console.WriteLine("======================================================");

            Console.ReadKey();
        }
    }
}
