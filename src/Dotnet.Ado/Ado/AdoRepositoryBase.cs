using Dotnet.Data;
using System;
using System.Data.Common;
using System.Linq.Expressions;
using System.Collections.Generic;
using Dotnet.Data.Providers;
using System.Linq;
using System.Threading.Tasks;
using Dotnet.Ado.Db;
using System.Data;
using Dotnet.Ado.Entity;
using Dotnet.Data.Expression;
using System.Dynamic;
using Dotnet.Reflecting;

namespace Dotnet.Ado
{

    public class AdoRepositoryBase<TEntity> : AdoRepositoryBase<TEntity, int> where TEntity : class, IEntity<int>, new() 
    {
    }

    public class AdoRepositoryBase<TEntity, TPrimaryKey> : RepositoryBase<TEntity, TPrimaryKey>  where TEntity : class, IEntity<TPrimaryKey>, new() 
    {

        public IActiveTransactionProvider _activeTransactionProvider { get; set; }

        public Database db { get; set; }

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
            SqlBuilder sqlBuilder = db.DbProvider.GetSQLByLambda<TEntity>(predicate);

            DbCommand dbCommand = db.GetSqlStringCommand(sqlBuilder.Sql);
            foreach (var item in sqlBuilder.DbParameters)
            {
                db.AddInParameter(dbCommand, item.ParameterName, item.Value);
            }

            return db.ExecuteReaderToEntity<TEntity>(dbCommand);
        }

        public override TEntity FirstOrDefault(TPrimaryKey id)
        {
            return FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        public override TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            QueryResult result = DynamicQuery.GetDynamicQuery(predicate);

            DbCommand dbCommand = db.GetSqlStringCommand(result.Sql);
            foreach (var item in result.Param)
            {
                db.AddInParameter(dbCommand, item.Key, item.Value);
            }

            return db.ExecuteReaderToEntity<TEntity>(dbCommand);
        }

        public override TEntity Get(TPrimaryKey id)
        {
            TEntity item = FirstOrDefault(id);
            return item;
        }

        public override IEnumerable<TEntity> GetAll()
        {
            SqlBuilder sqlBuilder = db.DbProvider.GetSQLByLambda<TEntity>(null);
            DbCommand dbCommand = db.GetSqlStringCommand(sqlBuilder.Sql);
            return db.ExecuteReader<TEntity>(dbCommand);
        }

        public override IEnumerable<TEntity> Query(string query, object parameters = null)
        {
            DbCommand dbCommand = db.GetSqlStringCommand(query);
            if (parameters != null)
            {
                foreach (var item in ReflectionHelper.GetObjectValues(parameters))
                {
                    db.AddInParameter(dbCommand, item.Key, item.Value);
                }
            }
            return db.ExecuteReader<TEntity>(dbCommand);
        }

        public override Task<IEnumerable<TEntity>> QueryAsync(string query, object parameters = null)
        {
            return Task.Factory.StartNew(() => Query(query, parameters));
        }

        public override IEnumerable<TAny> Query<TAny>(string query, object parameters = null)
        {
            /*  DbCommand dbCommand = db.GetSqlStringCommand(query);
              if (parameters != null)
              {
                  foreach (var item in parameters)
                  {
                      db.AddInParameter(dbCommand, item.Key, item.Value);
                  }
              return db.ExecuteReader<TAny>(dbCommand);
              }*/
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<TAny>> QueryAsync<TAny>(string query, object parameters = null)
        {
            return Task.Factory.StartNew(() => Query<TAny>(query, parameters));
        }

        public override int Execute(string query, object parameters = null)
        {
            DbCommand dbCommand = db.GetSqlStringCommand(query);
            if (parameters != null)
            {
                foreach (var item in ReflectionHelper.GetObjectValues(parameters))
                {
                    db.AddInParameter(dbCommand, item.Key, item.Value);
                }
            }
            return db.ExecuteNonQuery(dbCommand);
        }

        public override Task<int> ExecuteAsync(string query, object parameters = null)
        {
            return Task.Factory.StartNew(() => Execute(query, parameters));
        }

        public override IEnumerable<TEntity> GetAllPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, string sortingProperty, bool ascending = true)
        {
            throw new NotImplementedException();
        }

        public override int Count(Expression<Func<TEntity, bool>> predicate)
        {
            SqlBuilder sqlBuilder = db.DbProvider.GetExistsSQLByLambda<TEntity>(predicate);
            DbCommand dbCommand = db.GetSqlStringCommand(sqlBuilder.Sql);
            if (sqlBuilder.DbParameters != null && sqlBuilder.DbParameters.Count() > 0)
            {
                foreach (var item in sqlBuilder.DbParameters)
                {
                    db.AddInParameter(dbCommand, item.ParameterName, item.Value);
                }
            }
            return db.ExecuteNonQuery(dbCommand);
        }

        public override IEnumerable<TEntity> GetSet(Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, string sortingProperty, bool ascending = true)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            SqlBuilder sqlBuilder = db.DbProvider.GetSQLByLambda<TEntity>(predicate);
            DbCommand dbCommand = db.GetSqlStringCommand(sqlBuilder.Sql);
            if (sqlBuilder.DbParameters != null && sqlBuilder.DbParameters.Count() > 0)
            {
                foreach (var item in sqlBuilder.DbParameters)
                {
                    db.AddInParameter(dbCommand, item.ParameterName, item.Value);
                }
            }
            return db.ExecuteReader<TEntity>(dbCommand);
        }

        public override IEnumerable<TEntity> GetAllPaged(Expression<Func<TEntity, bool>> predicate, int pageNumber, int itemsPerPage, bool ascending = true, params Expression<Func<TEntity, object>>[] sortingExpression)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<TEntity> GetSet(Expression<Func<TEntity, bool>> predicate, int firstResult, int maxResults, bool ascending = true, params Expression<Func<TEntity, object>>[] sortingExpression)
        {
            throw new NotImplementedException();
        }

        public override void Insert(TEntity entity)
        {
            InsertAndGetId(entity);
        }

        public override void Update(TEntity entity)
        {
            SqlBuilder sqlBuilder = db.DbProvider.GetInsertSqlBuilder<TEntity>(entity);
            DbCommand dbCommand = db.GetSqlStringCommand(sqlBuilder.Sql);
            dbCommand.Parameters.AddRange(sqlBuilder.DbParameters.ToArray());
            db.ExecuteNonQuery(dbCommand);
        }

        public override void Delete(TEntity entity)
        {
            SqlBuilder sqlBuilder = db.DbProvider.GetDeleteSqlBuilder<TEntity>(entity);
            DbCommand dbCommand = db.GetSqlStringCommand(sqlBuilder.Sql);
            dbCommand.Parameters.AddRange(sqlBuilder.DbParameters.ToArray());
            db.ExecuteNonQuery(dbCommand);
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
            SqlBuilder sqlBuilder = db.DbProvider.GetInsertSqlBuilder<TEntity>(entity);
            DbCommand dbCommand = db.GetSqlStringCommand(sqlBuilder.Sql);
            dbCommand.Parameters.AddRange(sqlBuilder.DbParameters.ToArray());
            object obj=  db.ExecuteScalar(dbCommand);
            return  (TPrimaryKey)obj;  //这里会报错，有空再研究。哈哈
        }


        #region 执行command


        /// <summary>
        /// 执行ExecuteNonQuery
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(DbCommand cmd)
        {
            //int returnValue = 0;
            //using (DbTransaction tran = db.BeginTransaction())
            //{
            //    try
            //    {
            //        returnValue = ExecuteNonQuery(cmd, tran);
            //        tran.Commit();

            //    }
            //    catch
            //    {
            //        tran.Rollback();
            //        throw;
            //    }
            //}

            //return returnValue;
            if (null == cmd)
                return 0;

            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 执行ExecuteNonQuery
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(DbCommand cmd, DbTransaction tran)
        {
            if (null == cmd)
                return 0;
            return db.ExecuteNonQuery(cmd, tran);
        }

        /// <summary>
        /// 执行ExecuteScalar
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public object ExecuteScalar(DbCommand cmd, DbTransaction tran)
        {
            if (null == cmd)
                return null;

            return db.ExecuteScalar(cmd, tran);
        }

        /// <summary>
        /// 执行ExecuteScalar
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public object ExecuteScalar(DbCommand cmd)
        {
            //object returnValue = null;
            //using (DbTransaction tran = db.BeginTransaction())
            //{
            //    try
            //    {
            //        returnValue = ExecuteScalar(cmd, tran);
            //        tran.Commit();
            //    }
            //    catch
            //    {
            //        tran.Rollback();
            //        throw;
            //    }
            //}

            //return returnValue;
            if (null == cmd)
                return null;

            return db.ExecuteScalar(cmd);
        }

        /// <summary>
        /// 执行ExecuteReader
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(DbCommand cmd)
        {
            if (null == cmd)
                return null;
            return db.ExecuteReader(cmd);
        }

        /// <summary>
        /// 执行ExecuteReader
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(DbCommand cmd, DbTransaction tran)
        {
            if (null == cmd)
                return null;
            return db.ExecuteReader(cmd, tran);
        }

        /// <summary>
        /// 执行ExecuteDataSet
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(DbCommand cmd)
        {
            if (null == cmd)
                return null;
            return db.ExecuteDataSet(cmd);
        }

        /// <summary>
        /// 执行ExecuteDataSet
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="tran"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(DbCommand cmd, DbTransaction tran)
        {
            if (null == cmd)
                return null;
            return db.ExecuteDataSet(cmd, tran);
        }

        #endregion
    }
}
