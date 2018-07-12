using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Text;

namespace Dotnet.Data.Providers
{
    public abstract class BaseDbContext
    {
        public string connectionStringName = "DefaultConnectionString";

        public string ConnectionString
        {
            get {
                return ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            }
        }

        public DbProviderFactory GetDbFactory()
        {
            string _providerName = "";
            if (!string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName))
                _providerName = ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;
            else
                throw new Exception("ConnectionStrings中没有配置提供程序ProviderName！");

            return DbProviderFactories.GetFactory(_providerName);
        }
    }
}
