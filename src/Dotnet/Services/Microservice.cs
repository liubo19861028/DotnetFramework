using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Services
{
    /// <summary>
    /// Represents that the derived types are microservices.
    /// </summary>
    public abstract class Microservice : Service
    {
        private readonly ICommandConsumer commandConsumer;
        private readonly IEventConsumer eventConsumer;
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Microservice"/> class.
        /// </summary>
        /// <param name="commandConsumer">The command consumer.</param>
        /// <param name="eventConsumer">The event consumer.</param>
        public Microservice(ICommandConsumer commandConsumer, IEventConsumer eventConsumer)
        {
            this.commandConsumer = commandConsumer;
            this.eventConsumer = eventConsumer;
        }

        /// <summary>
        /// Gets the <see cref="ICommandConsumer"/> instance that can consume a command message.
        /// </summary>
        public ICommandConsumer CommandConsumer => commandConsumer;

        /// <summary>
        /// Gets the <see cref="IEventConsumer"/> instance that can consume an event message.
        /// </summary>
        public IEventConsumer EventConsumer => eventConsumer;

        /// <summary>
        /// Starts the service with specified arguments.
        /// </summary>
        /// <param name="args">The arguments with which the service will use
        /// to start.</param>
        public override void Start(object[] args)
        {
            this.commandConsumer.Subscriber.Subscribe();
            this.eventConsumer.Subscriber.Subscribe();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!disposed)
                {
                    this.commandConsumer.Dispose();
                    this.eventConsumer.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}
