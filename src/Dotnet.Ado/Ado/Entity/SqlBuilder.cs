using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Dotnet.Ado.Entity
{
    public class SqlBuilder
    {
        public string Sql { get; set; }
        public List<DbParameter> DbParameters { get; set; }
    }
}
