using Dotnet.Extensions;
using Dotnet.Runtime.Caching.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Dotnet.Redis.Configuration
{
    public class RedisCacheOptions
    {
        public ICachingConfiguration StartupConfiguration { get; }

        private const string ConnectionStringKey = "Redis.Cache";

        private const string DatabaseIdSettingKey = "Redis.Cache.DatabaseId";

        public string ConnectionString { get; set; }

        public int DatabaseId { get; set; }

        public RedisCacheOptions(ICachingConfiguration startupConfiguration)
        {
            StartupConfiguration = startupConfiguration;

            ConnectionString = GetDefaultConnectionString();
            DatabaseId = GetDefaultDatabaseId();
        }

        private static int GetDefaultDatabaseId()
        {
            var appSetting = ConfigurationManager.AppSettings[DatabaseIdSettingKey];
            if (appSetting.IsNullOrEmpty())
            {
                return -1;
            }

            int databaseId;
            if (!int.TryParse(appSetting, out databaseId))
            {
                return -1;
            }

            return databaseId;
        }

        private static string GetDefaultConnectionString()
        {
            var connStr = ConfigurationManager.ConnectionStrings[ConnectionStringKey];
            if (connStr == null || connStr.ConnectionString.IsNullOrWhiteSpace())
            {
                return "localhost";
            }

            return connStr.ConnectionString;
        }
    }
}
