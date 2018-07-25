using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Dotnet.Data.Providers;

namespace Dotnet.Ado.Provider
{
    public class ProviderFactory
    {
        #region Private Members

        private static Dictionary<string, DbProvider> providerCache = new Dictionary<string, DbProvider>();

        private ProviderFactory() { }

        #endregion

        /// <summary>
        /// 创建数据库事件提供程序
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="connectionString">The conn STR.</param>
        /// <param name="databaseType">The DatabaseType.</param>
        /// <returns>The db provider.</returns>
        public static DbProvider CreateDbProvider(string assemblyName, string className, string connectionString, DBType databaseType)
        {
           // Check.Require(connectionString, "connectionString", Check.NotNullOrEmpty);

            if (connectionString.IndexOf("microsoft.jet.oledb", StringComparison.OrdinalIgnoreCase) > -1 || connectionString.IndexOf(".db3", StringComparison.OrdinalIgnoreCase) > -1)
            {
               // Check.Require(connectionString.IndexOf("data source", StringComparison.OrdinalIgnoreCase) > -1, "ConnectionString的格式有错误，请查证！");

                string mdbPath = connectionString.Substring(connectionString.IndexOf("data source", StringComparison.OrdinalIgnoreCase) + "data source".Length + 1).TrimStart(' ', '=');
                if (mdbPath.ToLower().StartsWith("|datadirectory|"))
                {
                    mdbPath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\App_Data" + mdbPath.Substring("|datadirectory|".Length);
                }
                else if (connectionString.StartsWith("./") || connectionString.EndsWith(".\\"))
                {
                    connectionString = connectionString.Replace("/", "\\").Replace(".\\", AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\");
                }
                connectionString = connectionString.Substring(0, connectionString.ToLower().IndexOf("data source")) + "Data Source=" + mdbPath;
            }

            //如果是~则表示当前目录
            if (connectionString.Contains("~/") || connectionString.Contains("~\\"))
            {
                connectionString = connectionString.Replace("/", "\\").Replace("~\\", AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\");
            }

          
            string cacheKey = string.Concat(assemblyName, className, connectionString);
            if (providerCache.ContainsKey(cacheKey))
            {
                return providerCache[cacheKey];
            }
            else
            {
                System.Reflection.Assembly ass;

                ass = assemblyName == null ? typeof(DbProvider).Assembly
                    : System.Reflection.Assembly.Load(assemblyName);

                DbProvider retProvider = ass.CreateInstance(className, false, System.Reflection.BindingFlags.Default, null, new object[] { connectionString }, null, null) as DbProvider;
                providerCache.Add(cacheKey, retProvider);
                return retProvider;
            }
        }

        /// <summary>
        /// Gets the default db provider.
        /// </summary>
        /// <value>The default.</value>
        public static DbProvider Default
        {
            get
            {
                try
                {
#if NET40
                    if (ConfigurationManager.ConnectionStrings.Count > 0)
                    {
                        DbProvider dbProvider;
                        ConnectionStringSettings connStrSetting = ConfigurationManager.ConnectionStrings[ConfigurationManager.ConnectionStrings.Count - 1];
                        string[] assAndClass = connStrSetting.ProviderName.Split(',');
                        if (assAndClass.Length > 1)
                        {
                            dbProvider = CreateDbProvider(assAndClass[1].Trim(), assAndClass[0].Trim(), connStrSetting.ConnectionString, null);
                        }
                        else
                        {
                            dbProvider = CreateDbProvider(null, assAndClass[0].Trim(), connStrSetting.ConnectionString, null);
                        }

                        dbProvider.ConnectionStringsName = connStrSetting.Name;

                        return dbProvider;
                    }
#endif
                    return null;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
