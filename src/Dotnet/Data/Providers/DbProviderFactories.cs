using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Dotnet.Data.Providers
{
     public static class DbProviderFactories
     {
        /// <summary>
        /// 通过在appsettings.json文件中配置 "providerName",来创建对应的数据库链接
        /// </summary>
        /// <param name="providerInvariantName">例如：MySql.Data.MySqlClient</param>
        /// <returns>DbProviderFactory</returns>
        public static DbProviderFactory GetFactory(string providerInvariantName)
        {
            if (string.IsNullOrEmpty(providerInvariantName)) throw new Exception("数据库链接字符串配置不正确！");

            if (providerInvariantName.ToLower().Contains("mysql"))
            {
                return new MySqlClientFactory();
            }
            else if (providerInvariantName.ToLower().Contains("sqlclient"))
            {
                return SqlClientFactory.Instance;
            }

            throw new Exception("暂不支持您使用的数据库类型！");
        }
     }

}
