using Dotnet.Dependency;
using Dotnet.Serializing;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Redis.Runtime
{

    public class DefaultRedisCacheSerializer : IRedisCacheSerializer
    {
        private IJsonSerializer IJsonSerializer = null;

        public DefaultRedisCacheSerializer()
        {
            IJsonSerializer=  IocManager.GetContainer().Resolve<IJsonSerializer>();
        }

        public virtual object Deserialize(RedisValue objbyte)
        {
            return IJsonSerializer.DeserializeWithType(objbyte);
        }

        public virtual string Serialize(object value, Type type)
        {
            return IJsonSerializer.SerializeWithType(value, type);
        }
    }
}
