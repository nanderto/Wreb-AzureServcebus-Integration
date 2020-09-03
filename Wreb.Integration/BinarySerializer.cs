using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;

namespace Wreb.Integration
{
    public static class BinarySerializer
    {
        public static byte[] Serialize(ICommand command)
        {
            using (var memoryStream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, command);
                return memoryStream.ToArray();
            }
        }

        public static byte[] Serialize<T>(ICommand command)
        {
            var sb = SerializeToStringBuilder<T>((T)command);
            byte[] body = Encoding.UTF8.GetBytes(sb.ToString());
            return body;
        }

        public static ICommand Deserialize(byte[] serializedCommand)
        {
            using (var memoryStream = new MemoryStream(serializedCommand))
            {
                var formatter = new BinaryFormatter();
                return (ICommand)formatter.Deserialize(memoryStream);
            }
        }

        public static T Deserialize<T>(byte[] serializedCommand, List<Type> knownTypes)
        {
            string xmlData = (Encoding.UTF8.GetString(serializedCommand));
            return Deserialize<T>(xmlData, knownTypes);
        }

        /// <summary>Deserializes the specified XML data.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlData">The XML data.</param>
        /// <returns></returns>
        public static T Deserialize<T>(string xmlData, List<Type> knownTypes)
        {
            T obj;
            DataContractSerializer serializer = new DataContractSerializer(typeof(T), knownTypes);
            var reader = new XmlTextReader(new StringReader(xmlData));
            obj = (T)serializer.ReadObject(reader);
            return obj;
        }

        public static StringBuilder SerializeToStringBuilder<T>(T obj)
        {
            var sb = new StringBuilder();
            var xmlWrite = new XmlTextWriter(new StringWriter(sb));
            DataContractSerializer serializer = new DataContractSerializer(typeof(T));
            serializer.WriteObject(xmlWrite, obj);
            return sb;
        }
    }
}
