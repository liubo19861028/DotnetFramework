using System;
using System.Collections.Generic;

namespace Dotnet.Runtime.Caching
{
    public interface ICacheManager : IDisposable
    {
        IReadOnlyList<ICache> GetAllCaches();

        ICache GetCache(string name);
    }
}
