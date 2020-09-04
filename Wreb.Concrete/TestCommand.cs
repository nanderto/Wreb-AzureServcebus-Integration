using System;
using System.Runtime.Serialization;
using Wreb.Integration;

namespace Wreb.Concrete
{
    [Serializable]
    [KnownType(typeof(TestCommand))]
    public class TestCommand : Command, ICommand
    {
        public TestCommand(string originUser, string originSystem, string commandAction, string connectionId, int? id, string clientId) : 
            base(originUser, originSystem, commandAction, connectionId, id, clientId)
        {

        }

        public string TestProperty { get; set; }
    }

    [Serializable]
    [KnownType(typeof(TestCommand2))]
    public class TestCommand2 : Command, ICommand
    {
        public TestCommand2(
            string originUser, 
            string originSystem, 
            string commandAction, 
            string connectionId, 
            int? id, 
            string clientId,
            string testProperty) :
            base(originUser, originSystem, commandAction, connectionId, id, clientId)
        {
            this.TestProperty2 = testProperty;
        }

        public string TestProperty2 { get; set; }

    }
}