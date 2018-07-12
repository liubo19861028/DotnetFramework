using Dotnet.Dependency;
using Dotnet.Runtime.Caching.Configuration;

namespace Dotnet.Runtime.Caching.Memory
{
    public class DotnetMemoryCacheManager : CacheManagerBase
    {
        public DotnetMemoryCacheManager(ICachingConfiguration configuration) : base(configuration)
        {
        }

        protected override ICache CreateCacheImplementation(string name)
        {
            return IocManager.GetContainer().Resolve<DotnetMemoryCache>(name);
        }
    }
}
