using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Services
{
    /// <summary>
    /// Represents that the implemented classes are the command senders
    /// that will send a command to the message bus.
    /// </summary>
    public interface ICommandSender : IMessagePublisher
    {
    }
}
