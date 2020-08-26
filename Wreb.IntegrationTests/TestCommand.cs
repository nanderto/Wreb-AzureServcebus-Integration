using System;

namespace Wreb.Integration.Tests
{
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