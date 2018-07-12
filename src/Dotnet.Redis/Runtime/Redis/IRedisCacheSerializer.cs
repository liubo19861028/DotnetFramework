using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;

namespace Dotnet.Redis.Runtime
{

    public interface IRedisCacheSerializer
    {

        object Deserialize(RedisValue objbyte);


        string Serialize(object value, Type type);
    }
}
