using Dotnet.Data;
using System;
using System.Data.Common;
using System.Linq.Expressions;
using Dapper;
using DapperExtensions;
using System.Collections.Generic;
using Dotnet.Data.Providers;
using System.Linq;
using System.Threading.Tasks;


namespace Dotnet.Dapper
{
    public class DapperRepositoryBase<TEntity, TPrimaryKey> : RepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        /* private readonly IActiveTransactionProvider _activeTransactionProvider;
         public DapperRepositoryBase(IActiveTransactionProvider activeTransactionProvider)
         {
             _activeTransactionProvider = activeTransactionProvider;
         }*/

        public IActiveTransactionProvider _activeTransactionProvider { get; set; }

        public virtual DbConnection Connection
        {
            get { return (DbConnection)_activeTransactionProvider.GetActiveConnection(ActiveTransactionProviderArgs.Empty); }
        }

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

        public override TEntity Single(TPrimaryKey id)
        {
            return Single(CreateEqualityExpressionForId(id));
        }

        public override TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return Connection.GetList<TEntity>(predicate, transaction: ActiveTransaction).Single();
        }

        public override TEntity FirstOrDefault(TPrimaryKey id)
        {
            return FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        public override TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Connection.GetList<TEntity>(predicate, transaction: ActiveTransaction).FirstOrDefault();
        }

        public override TEntity Get(TPrimaryKey id)
        {
            TEntity item = FirstOrDefault(id);
            return item;
        }

        public override IEnumerable<TEntity> GetAll()
        {
            return Connection.GetList<TEntity>(null, transaction: ActiveTransaction);
        }

        public override IEnumerable<TEntity> Query(string query, object parameters = null)
        {
            return Connection.Query<TEntity>(query, parameters, ActiveTransaction);
        }

        public override Task<IEnumerable<TEntity>> QueryAsync(string query, object parameters = null)
        {
            return Connection.QueryAsync<TEntity>(query, parameters, ActiveTransaction);
        }

        public override IEnumerable<TAny> Query<TAny>(string query, object parameters = null)
        {
            return Connection.Query<TAny>(query, parameters, ActiveTransaction);
        }

        public override Task<IEnumerable<TAny>> QueryAsync<TAny>(string query, object parameters = null)
        {
            return Connection.QueryAsync<TAny>(query, parameters, ActiveTransaction);
        }

        public override int Execute(string query, object parameters = null)
        {
            return Connection.Execute(query, parameters, ActiveTransaction);
        }

        public override Task<int> ExecuteAsync(string query, object parameters = null)
        {
            return Connection.ExecuteAsync(query, parameters, ActiveTransaction);
        }

        public override IEnumerable<TEntity> GetAllPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, string sortingProperty, bool ascending = true)
        {
            return Connection.GetPage<TEntity>(
                predicate,
                new List<ISort> { new Sort { Ascending = ascending, PropertyName = sortingProperty } },
                pageNumber,
                itemsPerPage,
                ActiveTransaction);
        }

        public override int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return Connection.Count<TEntity>(predicate, ActiveTransaction);
        }

        public override IEnumerable<TEntity> GetSet(Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, string sortingProperty, bool ascending = true)
        {
            return Connection.GetSet<TEntity>(
                predicate,
                new List<ISort> { new Sort { Ascending = ascending, PropertyName = sortingProperty } },
                firstResult,
                maxResults,
                ActiveTransaction
            );
        }

        public override IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return Connection.GetList<TEntity>(predicate, transaction: ActiveTransaction);
        }

        public override IEnumerable<TEntity> GetAllPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, bool ascending = true, params Expression<Func<TEntity, object>>[] sortingExpression)
        {
            return Connection.GetPage<TEntity>(predicate, sortingExpression.ToSortable(ascending), pageNumber, itemsPerPage, ActiveTransaction);
        }

        public override IEnumerable<TEntity> GetSet(Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, bool ascending = true, params Expression<Func<TEntity, object>>[] sortingExpression)
        {
            return Connection.GetSet<TEntity>(predicate, sortingExpression.ToSortable(ascending), firstResult, maxResults, ActiveTransaction);
        }

        public override void Insert(TEntity entity)
        {
            InsertAndGetId(entity);
        }

        public override void Update(TEntity entity)
        {
            Connection.Update(entity, ActiveTransaction);
        }

        public override void Delete(TEntity entity)
        {
            Connection.Delete(entity, ActiveTransaction);
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
            TPrimaryKey primaryKey = Connection.Insert<TEntity>(entity, ActiveTransaction);
            return primaryKey;
        }
    }
}
