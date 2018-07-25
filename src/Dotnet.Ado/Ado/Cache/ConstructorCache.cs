using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.Ado.Cache
{
    /// <summary>
    /// 构造器缓存
    /// </summary>
    public class ConstructorCache : BaseCache<Type, Delegate>
    {
        protected override Delegate LoadData(Type key)
        {
            return GetDelegate(key);
        }

        protected Delegate GetDelegate(Type key)
        {
            var newExpression = Expression.New(key);
            return Expression.Lambda(newExpression, null).Compile();
        }
    }
}
