using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using Dotnet.Runtime.Caching;
using System.Reflection;
using Dotnet.Utility;
using Dotnet.Reflecting;
using Enyim.Caching;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using Enyim.Caching.Memcached;

namespace Dotnet.Memcached.Runtime
{
    public class MemcachedCache : CacheBase
    {
        private readonly MemcachedClient client;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MemcachedCache(
            string name,
            IMemcachedProvider IMemcachedProvider)
            : base(name)
        {
            client = IMemcachedProvider.GetClient();
        }

        public override object GetOrDefault(string key)
        {
            return client.Get(key);
        }

        public override void Set(string key, object value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null)
        {
            if (value == null)
            {
                throw new Exception("Can not insert null values to the cache!");
            }

            if (absoluteExpireTime != null)
            {
                client.Store(Enyim.Caching.Memcached.StoreMode.Set, key, value, DateTime.Now.Add(absoluteExpireTime.Value));
            }
            else if (slidingExpireTime != null)
            {
                client.Store(Enyim.Caching.Memcached.StoreMode.Set, key, value, slidingExpireTime.Value);
            }
            else if (DefaultAbsoluteExpireTime != null)
            {
                client.Store(Enyim.Caching.Memcached.StoreMode.Set, key, value, DateTime.Now.Add(DefaultAbsoluteExpireTime.Value));
            }
            else
            {
                client.Store(Enyim.Caching.Memcached.StoreMode.Set, key, value, DefaultSlidingExpireTime);
            }

        }

        public override void Remove(string key)
        {
            client.Remove(key);
        }

        public override void Clear()
        {
            client.FlushAll();
        }
        
        
        /// <summary>
        /// Check for item in cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        /// <returns>A boolean if the object exists</returns>
        public override  bool Exists(string key)
        {
            return client.Get(key) != null;
        }



    }

}
