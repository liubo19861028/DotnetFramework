using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Dotnet.Serializing
{
    public class DefaultJsonSerializer : IJsonSerializer
    {
        private const char TypeSeperator = '|';
        public string Serialize(object obj)
        {
            var serializer = new DataContractJsonSerializer(obj.GetType());
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, obj);
                stream.Position = 0;
                using (var sr = new StreamReader(stream))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        public object Deserialize(string value, Type type)
        {
            var serializer = new DataContractJsonSerializer(type);
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(value.ToCharArray())))
            {
                return serializer.ReadObject(stream);
            }
        }
        public T Deserialize<T>(string value) where T : class
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(value.ToCharArray())))
            {
                var obj = (T)serializer.ReadObject(stream);
                return obj;
            }
        }

        /// <summary>
        /// Serializes an object with a type information included.
        /// So, it can be deserialized using <see cref="DeserializeWithType"/> method later.
        /// </summary>
        public string SerializeWithType(object obj)
        {
            return SerializeWithType(obj, obj.GetType());
        }

        /// <summary>
        /// Serializes an object with a type information included.
        /// So, it can be deserialized using <see cref="DeserializeWithType"/> method later.
        /// </summary>
        public string SerializeWithType(object obj, Type type)
        {
            var serializer = new DataContractJsonSerializer(obj.GetType());
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, obj);
                stream.Position = 0;
                using (var sr = new StreamReader(stream))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Deserializes an object serialized with <see cref="SerializeWithType(object)"/> methods.
        /// </summary>
        public object DeserializeWithType(string serializedObj)
        {
            var typeSeperatorIndex = serializedObj.IndexOf(TypeSeperator);
            var type = Type.GetType(serializedObj.Substring(0, typeSeperatorIndex));
            var serializer = new DataContractJsonSerializer(type);
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(serializedObj.ToCharArray())))
            {
                return serializer.ReadObject(stream);
            }
        }


    }
}
