using Dotnet.Dependency;
using Dotnet.Memcached.Runtime;
using Dotnet.Redis.Configuration;
using Dotnet.Runtime.Caching;
using Dotnet.Runtime.Caching.Configuration;
using Dotnet.Runtime.Caching.Memory;
using System;

namespace Dotnet.Configurations
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Configures caching to use Redis as cache server.
        /// </summary>
        /// <param name="cachingConfiguration">The caching configuration.</param>
        public static Configuration UseMemcached(this Configuration configuration)
        {
            configuration.UseMemcached(options => { });
            return configuration;
        }

        /// <summary>
        /// Configures caching to use Redis as cache server.
        /// </summary>
        /// <param name="cachingConfiguration">The caching configuration.</param>
        /// <param name="optionsAction">Ac action to get/set options</param>
        public static void UseMemcached(this Configuration configuration, Action<MemcachedOptions> optionsAction)
        {
            var container = IocManager.GetContainer();

            container.Register<IMemcachedProvider, MemcachedProvider>();
            container.Register<ICacheManager, MemcachedManager>();

            optionsAction(container.Resolve<MemcachedOptions>());
        }


        

    }
}
