using Dotnet.Redis.Configuration;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Dotnet.Memcached.Runtime
{
    /// <summary>
    /// Implements <see cref="IMemcachedProvider"/>.
    /// </summary>
    public class MemcachedProvider : IMemcachedProvider
    {
        private readonly MemcachedOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemcachedProvider"/> class.
        /// </summary>
        public MemcachedProvider(MemcachedOptions options)
        {
            _options = options;
        }

        private static readonly ILoggerFactory _loggerFacotry = new LoggerFactory();

        /// <summary>
        /// Gets the database connection.
        /// </summary>
        public virtual MemcachedClient GetClient()
        {
            if (!_options.IsUseConfigSetting)
            {
                //初始化缓存
               string host = _options.ConnectionString;
                var options = new MemcachedClientOptions();
                MemcachedClientConfiguration memConfig = new MemcachedClientConfiguration(_loggerFacotry, options);
              //   IPAddress newaddress = IPAddress.Parse(Dns.GetHostEntry(host).AddressList[0].ToString());//your_ocs_host替换为OCS内网地址 
              //   IPEndPoint ipEndPoint = new IPEndPoint(newaddress, 11211);
                DnsEndPoint dnsEndPoint=new DnsEndPoint(host, 11211);

                // 配置文件 - ip
                memConfig.Servers.Add(dnsEndPoint);
                 // 配置文件 - 协议
                 memConfig.Protocol = MemcachedProtocol.Binary;
                 // 配置文件-权限
                 memConfig.Authentication.Type = typeof(PlainTextAuthenticator);
                 memConfig.Authentication.Parameters["zone"] = "";
                 memConfig.Authentication.Parameters["userName"] = _options.CacheServerUid;
                 memConfig.Authentication.Parameters["password"] = _options.CacheServerPwd;
                 //下面请根据实例的最大连接数进行设置
                 memConfig.SocketPool.MinPoolSize = 5;
                 memConfig.SocketPool.MaxPoolSize = 200;
                 return new MemcachedClient(_loggerFacotry, memConfig); 
            }
            else
            {
                var options = new MemcachedClientOptions();
                UtilConf.Configuration.GetSection("enyimMemcached").Bind(options);
                return new MemcachedClient(_loggerFacotry, new MemcachedClientConfiguration(_loggerFacotry, options));
            }
            
        }
    }

    public class UtilConf
    {
        private static IConfiguration config;
        public static IConfiguration Configuration//加载配置文件
        {
            get
            {
                if (config != null) return config;
                config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .Build();
                return config;
            }
            set => config = value;
        }
    }
}
