using Dotnet.Dependency;
using Dotnet.Serializing;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Redis.Runtime
{
    /// <summary>
    ///     Default implementation uses JSON as the underlying persistence mechanism.
    /// </summary>
    public class DefaultRedisCacheSerializer : IRedisCacheSerializer
    {
        private IJsonSerializer IJsonSerializer = null;

        public DefaultRedisCacheSerializer()
        {
            IJsonSerializer=  IocManager.GetContainer().Resolve<IJsonSerializer>();
        }


        /// <summary>
        ///     Creates an instance of the object from its serialized string representation.
        /// </summary>
        /// <param name="objbyte">String representation of the object from the Redis server.</param>
        /// <returns>Returns a newly constructed object.</returns>
        /// <seealso cref="IRedisCacheSerializer.Serialize" />
        public virtual object Deserialize(RedisValue objbyte)
        {
            return IJsonSerializer.DeserializeWithType(objbyte);
        }

        /// <summary>
        ///     Produce a string representation of the supplied object.
        /// </summary>
        /// <param name="value">Instance to serialize.</param>
        /// <param name="type">Type of the object.</param>
        /// <returns>Returns a string representing the object instance that can be placed into the Redis cache.</returns>
        /// <seealso cref="IRedisCacheSerializer.Deserialize" />
        public virtual string Serialize(object value, Type type)
        {
            return IJsonSerializer.SerializeWithType(value, type);
        }
    }
}
