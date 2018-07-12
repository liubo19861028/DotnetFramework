using Dotnet.Dependency;
using Dotnet.Runtime.Caching;
using Dotnet.Runtime.Caching.Configuration;

namespace Dotnet.Redis.Runtime
{
    public class RedisCacheManager : CacheManagerBase
    {
        public RedisCacheManager(ICachingConfiguration configuration) : base(configuration)
        {
        }

        protected override ICache CreateCacheImplementation(string name)
        {
            return IocManager.GetContainer().Resolve<RedisCache>(name);
        }
    }
}
