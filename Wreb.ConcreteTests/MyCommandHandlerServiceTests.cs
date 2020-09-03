using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wreb.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Wreb.Integration;

namespace Wreb.Concrete.Tests
{
    [TestClass]
    public class MyCommandHandlerServiceTests
    {
        [TestMethod]
        public void InitializeTest()
        {
            var myCommandHandlerService = new MyCommandHandlerService();
           // myCommandHandlerService.Initialize();
            Assert.AreEqual(typeof(TestCommand).FullName, myCommandHandlerService.KnownTypes[0].FullName);
            Assert.AreEqual(typeof(TestCommand2).FullName, myCommandHandlerService.KnownTypes[1].FullName);

            List<ISubscriber> subscriberList;
            Assert.IsTrue(myCommandHandlerService.TryGetSubscriberList(typeof(TestCommand2).FullName, out subscriberList));
            Assert.IsTrue(subscriberList.Count == 2);
            Assert.IsTrue(myCommandHandlerService.TryGetSubscriberList(typeof(TestCommand).FullName, out subscriberList));
            Assert.IsTrue(subscriberList.Count == 1);

            Assert.AreEqual(typeof(TestCommand).FullName, myCommandHandlerService.KnownTypes[0].FullName);
        }
    }
}