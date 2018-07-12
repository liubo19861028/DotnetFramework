using Dotnet.Redis.Configuration;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using System;
using System.Collections.Generic;
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


        /// <summary>
        /// Gets the database connection.
        /// </summary>
        public virtual MemcachedClient GetClient()
        {
            if (!_options.IsUseConfigSetting)
            {
                //初始化缓存
                string host = _options.ConnectionString;
                MemcachedClientConfiguration memConfig = new MemcachedClientConfiguration();
                IPAddress newaddress = IPAddress.Parse(Dns.GetHostEntry(host).AddressList[0].ToString());//your_ocs_host替换为OCS内网地址 
                IPEndPoint ipEndPoint = new IPEndPoint(newaddress, 11211);

                // 配置文件 - ip
                memConfig.Servers.Add(ipEndPoint);
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
                return new MemcachedClient(memConfig);
            }
            else
            {
                return new MemcachedClient();
            }
            
        }
    }
}
