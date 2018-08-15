using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Services
{
    /// <summary>
    /// Represents that the implemented classes are event publishers that
    /// can publish the domain events to the message bus.
    /// </summary>
    public interface IEventPublisher : IMessagePublisher
    {
    }
}
