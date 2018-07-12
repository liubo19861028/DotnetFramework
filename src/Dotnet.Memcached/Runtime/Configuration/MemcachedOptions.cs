using Dotnet.Extensions;
using Dotnet.Runtime.Caching.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Dotnet.Redis.Configuration
{
    public class MemcachedOptions
    {

        private const string ConnectionStringKey = "Memcached.Connection";
        private const string CacheServerUidKey = "Memcached.UID";
        private const string CacheServerPwdKey = "Memcached.PWD";
        public string ConnectionString { get; set; }
        public string CacheServerUid { get; set; }
        public string CacheServerPwd { get; set; }
        public bool IsUseConfigSetting { get; set; }

        public MemcachedOptions()
        {
            ConnectionString = GetDefaultConnectionString();
            CacheServerUid = GetDefaultCacheServerUid();
            CacheServerPwd = GetDefaultCacheServerPwdKey();
            IsUseConfigSetting = false;
        }

        private static string GetDefaultConnectionString()
        {
            return ConfigurationManager.AppSettings[ConnectionStringKey];;
        }

        private static string GetDefaultCacheServerUid()
        {
            return ConfigurationManager.AppSettings[CacheServerUidKey];
        }

        private static string GetDefaultCacheServerPwdKey()
        {
            return ConfigurationManager.AppSettings[CacheServerPwdKey];
        }
    }
}
