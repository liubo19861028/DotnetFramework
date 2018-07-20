using Dotnet.Data;
using System;
using System.Data.Common;
using System.Linq.Expressions;
using System.Collections.Generic;
using Dotnet.Data.Providers;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Dotnet.Ef
{
    public class EfRepositoryBase<TEntity> : EfRepositoryBase<TEntity, int> where TEntity : class, IEntity<int>
    {
    }

    public class EfRepositoryBase<TEntity, TPrimaryKey> : RepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {

        public EfActiveTransactionProvider _activeTransactionProvider { get; set; }

        public virtual DbContext Context
        {
            get { return (DbContext)_activeTransactionProvider.GetDbContext(ActiveTransactionProviderArgs.Empty); }
        }

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
            return Context.Set<TEntity>().AsNoTracking().FirstOrDefault(predicate);
        }

        public override TEntity FirstOrDefault(TPrimaryKey id)
        {
            return FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        public override TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().AsNoTracking().FirstOrDefault(predicate);
        }

        public override TEntity Get(TPrimaryKey id)
        {
            TEntity item = FirstOrDefault(id);
            return item;
        }

        public override IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().AsNoTracking().AsQueryable();
        }

        public override IEnumerable<TEntity> Query(string query, object parameters = null)
        {
            return null;
        }

        public override Task<IEnumerable<TEntity>> QueryAsync(string query, object parameters = null)
        {
            return Task.Factory.StartNew(() => Query(query, parameters));
        }

        public override IEnumerable<TAny> Query<TAny>(string query, object parameters = null)
        {
            return null;
        }

        public override Task<IEnumerable<TAny>> QueryAsync<TAny>(string query, object parameters = null)
        {
            return Task.Factory.StartNew(() => Query<TAny>(query, parameters));
        }

        public override int Execute(string query, object parameters = null)
        {
            return Context.Database.ExecuteSqlCommand(query);
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
            return this.GetAll(predicate).Count();
        }

        public override IEnumerable<TEntity> GetSet(Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, string sortingProperty, bool ascending = true)
        {
            return null;
        }

        public override IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            var dbSet = Context.Set<TEntity>().AsNoTracking().AsQueryable();
            if (predicate != null)
                dbSet = dbSet.Where(predicate);
            return dbSet;
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

        public void Save()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                throw new Exception(e.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage);
            }
        }

        public override void Update(TEntity entity)
        {
            var entry = this.Context.Entry(entity);
            entry.State = EntityState.Modified;

            //如果数据没有发生变化
            if (!this.Context.ChangeTracker.HasChanges())
            {
                return;
            }

            Save();
        }

        public override void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            Save();
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
            Context.Set<TEntity>().Add(entity);
            Save();
            return entity.Id;
        }
    }
}
