using Dotnet.Data.Providers;
using Dotnet.Dependency;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dotnet.Data
{
    public abstract class ServicesBase<TEntity> : ServicesBase<TEntity, int> where TEntity : class, IEntity<int>, new()
    {
    }
    /// <summary>
    ///     Base class to implement <see cref="IServices{TEntity,TPrimaryKey}" />.
    ///     It implements some methods in most simple way.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TPrimaryKey">The type of the primary key.</typeparam>
    /// <seealso cref="IServices{TEntity,TPrimaryKey}" />
    public abstract class ServicesBase<TEntity, TPrimaryKey> : IServices<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>, new()
    {
        public IRepository<TEntity, TPrimaryKey> Repository;
        private string _contextName = string.Empty;

        public ServicesBase()
        {
            Repository = IocManager.GetContainer().Resolve<IRepository<TEntity, TPrimaryKey>>();
        }

        public ServicesBase(IRepository<TEntity, TPrimaryKey> _repository)
        {
            Repository = _repository;
        }

        public ServicesBase(string contextName)
        {
            Repository = IocManager.GetContainer().Resolve<IRepository<TEntity, TPrimaryKey>>();
            _contextName = contextName;
        }

        private void SetDataContextByResloveName()
        {
            if (!string.IsNullOrEmpty(_contextName))
            {
                Repository.SetDataContextByResloveName(_contextName);
            }
        }


        public TEntity Single(TPrimaryKey id)
        {
            SetDataContextByResloveName();
            return Repository.Single(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            SetDataContextByResloveName();
            return Repository.GetAll();
        }

        public Task<TEntity> SingleAsync(TPrimaryKey id)
        {
            SetDataContextByResloveName();
            return Repository.SingleAsync(id);
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            SetDataContextByResloveName();
            return Repository.GetAllAsync();
        }

        public IEnumerable<TEntity> Query(string query, object parameters = null)
        {
            SetDataContextByResloveName();
            return Repository.Query(query, parameters);
        }

        public Task<IEnumerable<TEntity>> QueryAsync(string query, object parameters = null)
        {
            SetDataContextByResloveName();
            return Repository.QueryAsync(query, parameters);
        }

        public IEnumerable<TAny> Query<TAny>(string query, object parameters = null) where TAny : class, new()
        {
            SetDataContextByResloveName();
            return Repository.Query<TAny>(query, parameters);
        }

        public virtual Task<IEnumerable<TAny>> QueryAsync<TAny>(string query, object parameters = null) where TAny : class, new()
        {
            SetDataContextByResloveName();
            return Repository.QueryAsync<TAny>(query, parameters);
        }

        public int Execute(string query, object parameters = null)
        {
            SetDataContextByResloveName();
            return Repository.Execute(query, parameters);
        }

        public virtual Task<int> ExecuteAsync(string query, object parameters = null)
        {
            SetDataContextByResloveName();
            return Repository.ExecuteAsync(query, parameters);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            SetDataContextByResloveName();
            return Repository.GetAll(predicate);
        }

        public Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            SetDataContextByResloveName();
            return Repository.GetAllAsync(predicate);
        }

        public virtual Task<IEnumerable<TEntity>> GetAllPagedAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, string sortingProperty, bool ascending = true)
        {
            SetDataContextByResloveName();
            return Repository.GetAllPagedAsync(predicate, pageNumber, itemsPerPage, sortingProperty, ascending);
        }

        public IEnumerable<TEntity> GetAllPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, string sortingProperty, bool ascending = true)
        {
            SetDataContextByResloveName();
            return Repository.GetAllPaged(predicate, pageNumber, itemsPerPage, sortingProperty, ascending);
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            SetDataContextByResloveName();
            return Repository.Count(predicate);
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            SetDataContextByResloveName();
            return Repository.CountAsync(predicate);
        }

        public IEnumerable<TEntity> GetSet(Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, string sortingProperty, bool ascending = true)
        {
            SetDataContextByResloveName();
            return Repository.GetSet(predicate, firstResult, maxResults, sortingProperty, ascending = true);
        }

        public Task<IEnumerable<TEntity>> GetSetAsync(Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, string sortingProperty, bool ascending = true)
        {
            SetDataContextByResloveName();
            return Repository.GetSetAsync(predicate, firstResult, maxResults, sortingProperty, ascending = true);
        }

        public Task<IEnumerable<TEntity>> GetAllPagedAsync(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, bool ascending = true, params Expression<Func<TEntity, object>>[] sortingExpression)
        {
            SetDataContextByResloveName();
            return Repository.GetAllPagedAsync(predicate, pageNumber, itemsPerPage, ascending, sortingExpression);
        }

        public IEnumerable<TEntity> GetAllPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, bool ascending = true, params Expression<Func<TEntity, object>>[] sortingExpression)
        {
            SetDataContextByResloveName();
            return Repository.GetAllPaged(predicate, pageNumber, itemsPerPage, ascending, sortingExpression);
        }

        public IEnumerable<TEntity> GetSet(Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, bool ascending = true, params Expression<Func<TEntity, object>>[] sortingExpression)
        {
            SetDataContextByResloveName();
            return Repository.GetSet(predicate, firstResult, maxResults, ascending, sortingExpression);
        }

        public Task<IEnumerable<TEntity>> GetSetAsync(Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, bool ascending = true, params Expression<Func<TEntity, object>>[] sortingExpression)
        {
            SetDataContextByResloveName();
            return Repository.GetSetAsync(predicate, firstResult, maxResults, ascending, sortingExpression);
        }

        public void Insert(TEntity entity)
        {
            SetDataContextByResloveName();
            Repository.Insert(entity);
        }

        public Task InsertAsync(TEntity entity)
        {
            SetDataContextByResloveName();
            return Repository.InsertAsync(entity);
        }

        public TPrimaryKey InsertAndGetId(TEntity entity)
        {
            SetDataContextByResloveName();
            return Repository.InsertAndGetId(entity);
        }

        public Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            SetDataContextByResloveName();
            return Repository.InsertAndGetIdAsync(entity);
        }

        public void Update(TEntity entity)
        {
            SetDataContextByResloveName();
            Repository.Update(entity);
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            SetDataContextByResloveName();
            return Repository.UpdateAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            SetDataContextByResloveName();
            Repository.Delete(entity);
        }

        public Task DeleteAsync(TEntity entity)
        {
            SetDataContextByResloveName();
            return Repository.DeleteAsync(entity);
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            SetDataContextByResloveName();
            Repository.Delete(predicate);
        }

        public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            SetDataContextByResloveName();
            return Repository.DeleteAsync(predicate);
        }


        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            SetDataContextByResloveName();
            return Repository.Single(predicate);
        }

        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            SetDataContextByResloveName();
            return Repository.SingleAsync(predicate);
        }

        public TEntity FirstOrDefault(TPrimaryKey id)
        {
            SetDataContextByResloveName();
            return Repository.FirstOrDefault(id);
        }

        public Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            SetDataContextByResloveName();
            return Repository.FirstOrDefaultAsync(id);
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            SetDataContextByResloveName();
            return Repository.FirstOrDefaultAsync(predicate);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            SetDataContextByResloveName();
            return Repository.FirstOrDefault(predicate);
        }

        public TEntity Get(TPrimaryKey id)
        {
            SetDataContextByResloveName();
            return Repository.Get(id);
        }

        public Task<TEntity> GetAsync(TPrimaryKey id)
        {
            SetDataContextByResloveName();
            return Repository.GetAsync(id);
        }

    }
}
