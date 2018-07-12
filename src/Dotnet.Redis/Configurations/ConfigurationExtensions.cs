using Dotnet.Dependency;
using Dotnet.Redis.Configuration;
using Dotnet.Redis.Runtime;
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
        public static Configuration UseRedis(this Configuration configuration)
        {
            configuration.UseRedis(options => { });
            return configuration;
        }

        /// <summary>
        /// Configures caching to use Redis as cache server.
        /// </summary>
        /// <param name="cachingConfiguration">The caching configuration.</param>
        /// <param name="optionsAction">Ac action to get/set options</param>
        public static void UseRedis(this Configuration configuration, Action<RedisCacheOptions> optionsAction)
        {
            var container = IocManager.GetContainer();

            container.Register<IRedisCacheDatabaseProvider, RedisCacheDatabaseProvider>();
            container.Register<ICacheManager, RedisCacheManager>();
            container.Register<ICache, RedisCache>();

            optionsAction(container.Resolve<RedisCacheOptions>());
        }


        

    }
}
