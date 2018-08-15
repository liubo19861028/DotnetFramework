using Dotnet.Data.Providers;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Dotnet.NHibernate
{
    public class NHibernateActiveTransactionProvider : IActiveTransactionProvider
    {
        public IDbContext DbContext { get; set; }
        private ISession _session;
        public ISessionFactoryHolder _sessionFactoryHolder { get; set; }

        public NHibernateActiveTransactionProvider(IDbContext dbContext)
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
            return null;
        }

        public ISession GetActiveSession(ActiveTransactionProviderArgs args)
        {
            EnsureSession();
            return _session;
        }

        private void EnsureSession()
        {
            if (_session != null)
            {
                return;
            }

            var sessionFactory = _sessionFactoryHolder.GetSessionFactory();
            _session = sessionFactory.OpenSession();

        }
    }
}
