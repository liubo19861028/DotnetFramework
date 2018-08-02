using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotColumnAttribute:Attribute
    {
        public NotColumnAttribute()
        {
        
        }
       
    }
}
