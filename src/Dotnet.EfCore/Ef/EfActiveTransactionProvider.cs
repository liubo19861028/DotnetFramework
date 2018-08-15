using Dotnet.Data.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Dotnet.Ef
{
    public class EfActiveTransactionProvider: IActiveTransactionProvider
    {
        public IDbContext DbContext { get; set; }

        public EfActiveTransactionProvider(IDbContext dbContext)
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
            return GetDbContext(args).Database.GetDbConnection();
        }

        public DbContext GetDbContext(ActiveTransactionProviderArgs args)
        {
            DbContext dbContext = (DbContext)DbContext;
            return dbContext;
        }
    }
}
