using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet
{
    /// <summary>
    /// 序列for oracle
    /// </summary>
    public class SequenceAttribute : Attribute
    {
        public string Name { get; set; }
        public SequenceAttribute(string name)
        {
            Name = name;
        }
    }
}
