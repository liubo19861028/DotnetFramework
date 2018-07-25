using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Dotnet.Data.Providers
{
    public interface IDbContext
    {
        DbProviderFactory GetDbFactory();
        string ConnectionString { get; }
        DBType DBType { get; set; }
    }
}
