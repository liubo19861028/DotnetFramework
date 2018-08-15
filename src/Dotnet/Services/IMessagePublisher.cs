using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Services
{
    /// <summary>
    /// Represents that the implemented classes are message publishers that
    /// can publish the specified message to the message bus.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IMessagePublisher : IDisposable
    {
        /// <summary>
        /// Publishes the specified message to the message queue.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message to be published.</typeparam>
        /// <param name="message">The message to be published.</param>
        void Publish<TMessage>(TMessage message);
    }
}
