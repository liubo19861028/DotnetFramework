using System;
using System.Data.Common;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dotnet.Data;
using Dotnet.Data.Providers;
using NHibernate;
using NHibernate.Linq;

namespace Dotnet.NHibernate
{
    public class NHibernateRepositoryBase<TEntity, TPrimaryKey> : RepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        public NHibernateActiveTransactionProvider _activeTransactionProvider { get; set; }


        /// <summary>
        ///     Gets the active transaction. If Dapper is active then <see cref="IUnitOfWork" /> should be started before
        ///     and there must be an active transaction.
        /// </summary>
        /// <value>
        ///     The active transaction.
        /// </value>
        public virtual DbTransaction ActiveTransaction
        {
            get { return (DbTransaction)_activeTransactionProvider.GetActiveTransaction(ActiveTransactionProviderArgs.Empty); }
        }

        public virtual DbConnection ActiveConnection
        {
            get { return (DbConnection)_activeTransactionProvider.GetActiveConnection(ActiveTransactionProviderArgs.Empty); }
        }


        protected virtual ISession Session
        {
            get { return (ISession)_activeTransactionProvider.GetActiveSession(ActiveTransactionProviderArgs.Empty); }
        }

        public virtual IQueryable<TEntity> Table
        {
            get { return Session.Query<TEntity>().Cacheable(); }
        }

        public virtual IQueryable<TEntity> Fetch(Expression<Func<TEntity, bool>> predicate)
        {
            return Table.Where(predicate);
        }

        public virtual IQueryable<TEntity> Fetch(Expression<Func<TEntity, bool>> predicate, Action<Orderable<TEntity>> order)
        {
            var orderable = new Orderable<TEntity>(Fetch(predicate));
            order(orderable);
            return orderable.Queryable;
        }

        public virtual IQueryable<TEntity> Fetch(Expression<Func<TEntity, bool>> predicate, Action<Orderable<TEntity>> order, int skip,int count)
        {
            return Fetch(predicate, order).Skip(skip).Take(count);
        }



        public override TEntity Single(TPrimaryKey id)
        {
            return Single(CreateEqualityExpressionForId(id));
        }

        public override TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return Fetch(predicate).SingleOrDefault();
        }

        public override TEntity FirstOrDefault(TPrimaryKey id)
        {
            return FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        public override TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Fetch(predicate).SingleOrDefault();
        }

        public override TEntity Get(TPrimaryKey id)
        {
            TEntity item = FirstOrDefault(id);
            return item;
        }

        public override IEnumerable<TEntity> GetAll()
        {
            return GetAll(null);
        }

        public override IEnumerable<TEntity> Query(string query, object parameters = null)
        {
            return null;
        }

        public override Task<IEnumerable<TEntity>> QueryAsync(string query, object parameters = null)
        {
            return null;
        }

        public override IEnumerable<TAny> Query<TAny>(string query, object parameters = null)
        {
            return null;
        }

        public override Task<IEnumerable<TAny>> QueryAsync<TAny>(string query, object parameters = null)
        {
            return null;
        }

        public override int Execute(string sql, object parameters = null)
        {
            ISQLQuery query = Session.CreateSQLQuery(sql);
            return query.ExecuteUpdate();
        }

        public override Task<int> ExecuteAsync(string query, object parameters = null)
        {
            return Task.Factory.StartNew(() => Execute(query, parameters));
        }

        public override IEnumerable<TEntity> GetAllPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, string sortingProperty, bool ascending = true)
        {
            return null;
        }

        public override int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return Fetch(predicate).Count();
        }

        public override IEnumerable<TEntity> GetSet(Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, string sortingProperty, bool ascending = true)
        {
            return null;
        }

        public override IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return Fetch(predicate);
        }

        public override IEnumerable<TEntity> GetAllPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, bool ascending = true, params Expression<Func<TEntity, object>>[] sortingExpression)
        {
            return null;
        }

        public override IEnumerable<TEntity> GetSet(Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, bool ascending = true, params Expression<Func<TEntity, object>>[] sortingExpression)
        {
            return null;
        }

        public override void Insert(TEntity entity)
        {
            InsertAndGetId(entity);
        }

        public override void Update(TEntity entity)
        {
            Session.Evict(entity);
            Session.Merge(entity);
        }

        public override void Delete(TEntity entity)
        {
            Session.Delete(entity);
        }

        public override void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> items = GetAll(predicate);
            foreach (TEntity entity in items)
            {
                Delete(entity);
            }
        }

        public override TPrimaryKey InsertAndGetId(TEntity entity)
        {
            Session.Save(entity);
            return entity.Id;
        }
    }
}
