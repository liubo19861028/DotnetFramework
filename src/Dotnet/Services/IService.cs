using System;

namespace Dotnet.Services
{
    /// <summary>
    /// Represents that the implemented classes are services.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IService : IDisposable
    {
        /// <summary>
        /// Starts the service with specified arguments.
        /// </summary>
        /// <param name="args">The arguments with which the service will use
        /// to start.</param>
        void Start(object[] args);
    }
}
