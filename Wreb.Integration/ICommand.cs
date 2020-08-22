using System;

namespace Wreb.Integration
{
    public interface ICommand
    {
        string ClientId { get; set; }
        string CommandAction { get; set; }
        string ConnectionId { get; set; }
        DateTimeOffset CreateDateTimeOffset { get; set; }
        int? Id { get; set; }
        string OriginSystem { get; set; }
        string OriginUser { get; set; }
        string UniqueKey { get; set; }
    }
}