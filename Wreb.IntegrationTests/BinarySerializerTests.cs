using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wreb.Integration;
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
}