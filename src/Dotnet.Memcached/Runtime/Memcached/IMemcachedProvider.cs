using Enyim.Caching;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Memcached.Runtime
{
    /// <summary>
    /// Used to get <see cref="IDatabase"/> for Redis cache.
    /// </summary>
    public interface IMemcachedProvider
    {
        /// <summary>
        /// Gets the database connection.
        /// </summary>
        MemcachedClient GetClient();
    }
}
