using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute:Attribute
    {
       
        public TableAttribute(string tableName)
        {
            this.Name = tableName;
        }
        public string Name { get; set; }
    }
}
