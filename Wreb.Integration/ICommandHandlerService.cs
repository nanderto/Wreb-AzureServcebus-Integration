using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wreb.Integration
{
    public interface ICommandHandlerService
    {
        List<Type> KnownTypes { get; set; }

        Task HandleCommandAsync(ICommand command);
    }
}