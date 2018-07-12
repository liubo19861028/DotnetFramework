using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;

namespace Dotnet.Redis.Runtime
{
    /// <summary>
    /// Used to get <see cref="IDatabase"/> for Redis cache.
    /// </summary>
    public interface IRedisCacheDatabaseProvider
    {
        /// <summary>
        /// Gets the database connection.
        /// </summary>
        IDatabase GetDatabase();
    }
}
