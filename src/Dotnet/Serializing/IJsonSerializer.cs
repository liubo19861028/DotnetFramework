using System;

namespace Dotnet.Serializing
{
    /// <summary>Json序列化接口
    /// </summary>
    public interface IJsonSerializer
    {
        string Serialize(object obj);
        string SerializeWithType(object value, Type type);
        object Deserialize(string value, Type type);
        object DeserializeWithType(string value);
        T Deserialize<T>(string value) where T : class;
    }
}
