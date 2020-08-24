using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wreb.Integration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wreb.Integration.Tests
{
    [TestClass]
    public class CommanderTests
    {
        [TestMethod]
        public void SendAsyncTest()
        {
            var commander = new Commander();

            var testCommand = new TestCommand("originuserx", "originsystemx", Command.Actions.Add, "connectionidx", null, "clientidx")
            {
                TestProperty = "XXXX"
            };
        }
    }
}