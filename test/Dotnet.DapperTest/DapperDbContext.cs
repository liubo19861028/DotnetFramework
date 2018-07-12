using Dotnet.Data.Providers;
using Dotnet.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Dotnet.DapperTest
{
    public  class DapperDbContext: BaseDbContext, IDbContext
    {

        public DapperDbContext()
        {
             base.connectionStringName = "test";
        }
    }
}
