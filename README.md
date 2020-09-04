# Wreb-AzureServcebus-Integration

The Wreb.Integration library is used to create commands, send them to Azure Service bus and to read them from the service bus and run any handlers that are subscribed to those commands.

## Usage
1. You need to create a command. A command is a DTO that carries from the calling application the data that needs to be updated and the type of command that it is.
To create this command you need to inherit from Command and ICommand
```csharp
    [Serializable]
    [KnownType(typeof(TestCommand))]
    public class TestCommand : Command, ICommand
    {
        public TestCommand(
            string originUser, 
            string originSystem, 
            string commandAction, 
            string connectionId, 
            int? id, 
            string clientId) : 
            base(originUser, originSystem, commandAction, connectionId, id, clientId)
        {

        }

        public string TestProperty { get; set; }
    }
```
Currently we are using the DataContractSerializer so to ensure that commands can be create that confirm to the ICommand you need to add the KnownType attribute.

2. You also need a Subscriber that will subscribe to the command and execute something after reading the command from Azure Servicebus.
The subscriber needs to inherit from SubscriberBase and from ISubscriber
```csharp
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Wreb.Integration;

namespace Wreb.Concrete
{
    public class ConsoleWriterSubscriber : SubscriberBase, ISubscriber
    {
        public override async Task ExecuteAsync(ICommand command)
        {
            Console.WriteLine($"Executing subscriber ConsoleWriterSubscriber, the ClientId of the command is {command.ClientId} ");
        }
    }

    public class ConsoleWriterSubscriber2 : SubscriberBase, ISubscriber
    {
        public override async Task ExecuteAsync(ICommand command)
        {
            Console.WriteLine($"Executing subscriber ConsoleWriterSubscriber2, the ClientId of the command is {command.ClientId} ");
        }
    }
}
```
There are 2 subscribers shown, each of them just write a message out to the console. you can see you have access to the data in the command, which includes all of the base data like create date time and any data you added (just cast it to your command). 

3. Finally you need to create or update the CommandHandlerService it is responsible for wireing these two things together.
The CommandHandlerService that you create must inherit from the CommandHandlerService and ICommandHandlerService. Here it is called MyCommandHandlerService.

```csharp
namespace Wreb.Concrete
{
    public class MyCommandHandlerService : CommandHandlerService, ICommandHandlerService
    {
        public override void Initialize()
        {
            Add(new ConsoleWriterSubscriber().WithYourCommandHandlerService(this).SubscribeToCommand(typeof(TestCommand)));
            Add(new ConsoleWriterSubscriber().WithYourCommandHandlerService(this).SubscribeToCommand(typeof(TestCommand2)));
            Add(new ConsoleWriterSubscriber2().WithYourCommandHandlerService(this).SubscribeToCommand(typeof(TestCommand2)));
        }
    }
}
```

To wire up the commands and subscribers override the Initialize method and Add a new subscriber that has had the CommandHandlerService Attached to it and the Command that you intend to subscribe to.

4. Finally you need to get this running in a Azure Function or a console application. To make it work in a Azure function, First create the Azure function, Use the Visual Studio Templates (or craft your own). Then add a Startup class to handle the dependency injection.
```csharp
public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddSingleton<ICommandHandlerService>((s) => {
            return new MyCommandHandlerService();
        });

        //builder.Services.AddSingleton<ILoggerProvider, MyLoggerProvider>();
    }
}
```
We create a builder and map our service to the interface so we will get it on creation of the function.
Each message that comes thru will cause a new Function to be created and a new CommandHandlerService will be injected in for each message. 

5. Your template will also have created a class that has the function in it.
```csharp
public class CommandReceiver
    {
        private readonly ICommandHandlerService commandHandlerService;

        public CommandReceiver(ICommandHandlerService commandHandlerService)
        {
            this.commandHandlerService = commandHandlerService;
        }

        [FunctionName("CommandReceiver")]
        public async Task RunAsync([ServiceBusTrigger("firstqueue", Connection = "AzureServicebusConnection")] Message myQueueItem, ILogger log)
        {

            ICommand command = BinarySerializer.Deserialize<ICommand>(myQueueItem.Body, this.commandHandlerService.KnownTypes);

            await this.commandHandlerService.HandleCommandAsync(command);
        }
    }
```
Here there are a few things to note. first the templates create static classes, this needs to be changed to allow the dependency injection to work, and to be able to access the service that is injected. Note you need to add a constructor that gets the CommandHandlerService and saves it for use in the functions. In the function you need to use the BinarySerializer to deserialize the message and then use the Command handlerService.HandleCommandAsync() method to handle the deserialized command. This method will call the subscribers added in step 2 and call there execute method and pass in the command.
