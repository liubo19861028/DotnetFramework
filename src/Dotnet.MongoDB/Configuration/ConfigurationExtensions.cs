using Dotnet.Data;
using Dotnet.Dependency;
using Dotnet.MongoDb.Repositories;
using System;

namespace Dotnet.Configurations
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Configures caching to use Redis as cache server.
        /// </summary>
        /// <param name="cachingConfiguration">The caching configuration.</param>
        public static Configuration UseMongoDB(this Configuration configuration)
        {
            var container = IocManager.GetContainer();

            container.Register(typeof(IRepository<>), typeof(MongoDbRepositoryBase<>), DependencyLifeStyle.Transient);

            return configuration;
        }



        

    }
}
