using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Wreb.Concrete;
using Wreb.Integration;

[assembly: FunctionsStartup(typeof(Wreb.Servicebus.Startup))]

namespace Wreb.Servicebus
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
           // builder.Services.AddHttpClient();

            builder.Services.AddSingleton<ICommandHandlerService>((s) => {
                return new MyCommandHandlerService();
            });

            //builder.Services.AddSingleton<ILoggerProvider, MyLoggerProvider>();
        }
    }
}