namespace  Dotnet.Services
{
    /// <summary>
    /// Represents that the derived types are services.
    /// </summary>
    public abstract class Service : DisposableObject, IService
    {
        /// <summary>
        /// Starts the service with specified arguments.
        /// </summary>
        /// <param name="args">The arguments with which the service will use
        /// to start.</param>
        public abstract void Start(object[] args);
    }
}
