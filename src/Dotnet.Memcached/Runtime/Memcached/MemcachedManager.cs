using Dotnet.Dependency;
using Dotnet.Runtime.Caching;
using Dotnet.Runtime.Caching.Configuration;

namespace Dotnet.Memcached.Runtime
{
    public class MemcachedManager : CacheManagerBase
    {
        public MemcachedManager(ICachingConfiguration configuration) : base(configuration)
        {
        }

        protected override ICache CreateCacheImplementation(string name)
        {
            return IocManager.GetContainer().Resolve<MemcachedCache>(name);
        }
    }
}
