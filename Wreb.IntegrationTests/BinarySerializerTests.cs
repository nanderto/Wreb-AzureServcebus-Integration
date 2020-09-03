using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wreb.Integration;
using System.Collections.Generic;
using System.Text;
using System;
using Wreb.Concrete;

namespace Wreb.Integration.Tests
{
    [TestClass]
    public class BinarySerializerTests
    {

        [TestMethod]
        public void SerializeTest()
        {
            var testCommand = new TestCommand("originuserx","originsystemx", Command.Actions.Add, "connectionidx", null, "clientidx")
            {
                TestProperty = "ZZZ"
            };

            TestCommand actual = (TestCommand)BinarySerializer.Deserialize(BinarySerializer.Serialize(testCommand));
            Assert.AreEqual(testCommand, actual);
        }

        [TestMethod]
        public void SerializeWithDataContractSerializerTest()
        {
            var testCommand = new TestCommand("originuserx", "originsystemx", Command.Actions.Add, "connectionidx", null, "clientidx")
            {
                TestProperty = "ZZZ"
            };
            
            var knownTypes = new List<Type>();
            knownTypes.Add(typeof(TestCommand));

            string s = BinarySerializer.SerializeToStringBuilder(testCommand).ToString();
            byte[] serializedCommand = Encoding.UTF8.GetBytes(s);
            string xmlData = Encoding.UTF8.GetString(serializedCommand);
            var actual = BinarySerializer.Deserialize<TestCommand>(xmlData, knownTypes);
            Assert.AreEqual(testCommand, actual);
        }

        [TestMethod]
        public void SerializeWithDataContractSerializerOfTypeTest()
        {
            var testCommand = new TestCommand("originuserx", "originsystemx", Command.Actions.Add, "connectionidx", null, "clientidx")
            {
                TestProperty = "ZZZ"
            };


            var knownTypes = new List<Type>();
            knownTypes.Add(typeof(TestCommand));

            byte[] serializedCommand = BinarySerializer.Serialize<ICommand>(testCommand);

            var actual = (TestCommand)BinarySerializer.Deserialize<ICommand>(serializedCommand, knownTypes);
            Assert.AreEqual(testCommand, actual);
            Assert.AreEqual(testCommand.GetHashCode(), actual.GetHashCode());
        }
    }
}   