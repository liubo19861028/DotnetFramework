using Dotnet.Data.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Dotnet.Dapper
{
    public class DapperActiveTransactionProvider: IActiveTransactionProvider
    {
        public IDbContext DbContext { get; set; }

        private DbProviderFactory DbFactory {
            get { return DbContext.GetDbFactory(); }
        }

        public DapperActiveTransactionProvider(IDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public IDbTransaction GetActiveTransaction(ActiveTransactionProviderArgs args)
        {
            //return GetActiveConnection(args).BeginTransaction();
            return null;
        }

        public IDbConnection GetActiveConnection(ActiveTransactionProviderArgs args)
        {
           var connection = DbFactory.CreateConnection();
            connection.ConnectionString = DbContext.ConnectionString;
            if (connection.State != ConnectionState.Open)
                connection.Open();

            return connection;
        }
    }
}
