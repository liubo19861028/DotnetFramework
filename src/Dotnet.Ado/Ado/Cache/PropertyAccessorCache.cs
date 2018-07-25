using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.Ado.Cache
{
    public class PropertyAccessorCache : BaseCache<PropertyInfo, IPropertyAccessor>
    {
        protected override IPropertyAccessor LoadData(PropertyInfo key)
        {
            //return new PropertyAccessorEmit(key);
            return new PropertyAccessorExpression(key);
        }
    }
}
