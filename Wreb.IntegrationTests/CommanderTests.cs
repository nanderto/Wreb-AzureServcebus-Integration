using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wreb.Integration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Wreb.Integration.Tests
{
    [TestClass]
    public class CommanderTests
    {
        private static IConfiguration config = null;

        private static string ConnectionString = string.Empty;

        private static string QueueName = string.Empty;

        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            ConnectionString = config.GetConnectionString("AzureServicebusConnection");
            QueueName = config.GetSection("QueueName").Value;
        }

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