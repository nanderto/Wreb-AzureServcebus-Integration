using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wreb.Integration;
using System;
using System.Collections.Generic;
using System.Text;

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
    }

    [Serializable]
    public class TestCommand : Command, ICommand
    {

        public TestCommand(string originUser, string originSystem, string commandAction, string connectionId, int? id, string clientId) : 
            base(originUser, originSystem, commandAction, connectionId, id, clientId)
        {

        }

        public string TestProperty { get; set; }

        public override bool Equals(object obj)
        {
            return obj is TestCommand command &&
                   TestProperty == command.TestProperty &&
                   base.Equals(command);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}