using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wreb.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wreb.Concrete.Tests
{
    [TestClass()]
    public class MyCommandHandlerServiceTests
    {
        [TestMethod()]
        public void InitializeTest()
        {
            var myCommandHandlerService = new MyCommandHandlerService();
           // myCommandHandlerService.Initialize();
            Assert.AreEqual(typeof(TestCommand).FullName, myCommandHandlerService.KnownTypes[0].FullName);
        }
    }
}