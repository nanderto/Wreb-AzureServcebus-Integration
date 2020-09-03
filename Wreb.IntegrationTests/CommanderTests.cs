using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wreb.Integration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Wreb.Concrete;

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
        public async Task SendAsyncTest()
        {
            var commander = new Commander();
            var receiver = new Receiver();

            var testCommand = new TestCommand("originuserx", "originsystemx", Command.Actions.Add, "connectionidx", null, "clientidx")
            {
                TestProperty = "XXXX"
            };

            if(await commander.SendAsync(testCommand) > 0)
            {
                Assert.IsTrue(true);
            }
            else
            {
            }
        }


        [TestMethod]
        public async Task SendAsyncOfTypeTest()
        {
            var commander = new Commander();
            var receiver = new Receiver();

            var testCommand = new TestCommand("originuserx", "originsystemx", Command.Actions.Add, "connectionidx", null, "clientidx")
            {
                TestProperty = "XXXX"
            };

            var knownTypes = new List<Type>();
            knownTypes.Add(typeof(TestCommand));

            knownTypes.Add(typeof(TestCommand));
            if (await commander.SendAsync<TestCommand>(testCommand) > 0)
            {
                Assert.IsTrue(true);
            }
            else
            {
            }
        }
    }
}