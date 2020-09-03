using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wreb.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wreb.Concrete.Tests
{
    [TestClass]
    public class ConsoleWriterSubscriberTests
    {
        [TestMethod]
        public void SubscribeToCommandTest()
        {
            var commandHandlerService = new MyCommandHandlerService();
            Assert.AreEqual(commandHandlerService.KnownTypes[0], typeof(TestCommand));
        }
    }
}