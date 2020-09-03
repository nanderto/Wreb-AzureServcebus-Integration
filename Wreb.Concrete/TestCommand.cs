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
            this.TestProperty = testProperty;
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