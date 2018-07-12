using Dotnet.Dependency;
using Dotnet.Redis.Configuration;
using Dotnet.Redis.Runtime;
using Dotnet.Runtime.Caching;
using System;

namespace Dotnet.Configurations
{
    public static class ConfigurationExtensions
    {
        public static Configuration UseRedis(this Configuration configuration)
        {
            configuration.UseRedis(options => { });
            return configuration;
        }

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
