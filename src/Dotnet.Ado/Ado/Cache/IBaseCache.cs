using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Ado.Cache
{
    /// <summary>
    /// 缓存接口
    /// </summary>
    interface IBaseCache<TKey, TValue>
    {
        void Clear();

        TValue Get(TKey key);

        bool Exists(TKey key);

        void Remove(TKey key);

        void Insert(TKey key, TValue value);

    }
}
