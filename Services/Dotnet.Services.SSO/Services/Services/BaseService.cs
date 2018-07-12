using System;
using System.Linq;
using Dotnet.Data;

namespace Dotnet.Services.SSO
{
    /// <summary>
    /// 业务层基类，UnitWork用于事务操作，Repository用于普通的数据库操作
    /// <para>如用户管理：Class UserManagerService:BaseService<User，int></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseService<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 用于普通的数据库操作
        /// </summary>
        /// <value>The repository.</value>
        public RepositoryBase<TEntity, TPrimaryKey> Repository;

       
    }
}
