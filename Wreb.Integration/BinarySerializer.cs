using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Wreb.Integration
{
    public static class BinarySerializer
    {
        public static byte[] Serialize(ICommand command)
        {
            using(var memoryStream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, command);
                return memoryStream.ToArray();
            }
        }

        public static ICommand Deserialize(byte[] serializedCommand)
        {
            using (var memoryStream = new MemoryStream(serializedCommand))
            {
                var formatter = new BinaryFormatter();
                return (ICommand) formatter.Deserialize(memoryStream);
            }
        }
    }
}
