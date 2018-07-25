using Dotnet.Ado.Cache;
using Dotnet.Ado.Entity;
using Dotnet.Data.Expression;
using Dotnet.Data;
using Dotnet.Data.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Dotnet.Ado.Provider
{
    /// <summary>
    /// DbProvider
    /// </summary>
    public abstract class DbProvider : IDisposable
    {


        #region Protected Members
        /// <summary>
        /// like符号。 
        /// </summary>
        protected char likeToken;
        /// <summary>
        /// 【
        /// </summary>
        protected char leftToken;
        /// <summary>
        /// 参数前缀
        /// </summary>
        protected char paramPrefixToken;

        /// <summary>
        /// 】
        /// </summary>
        protected char rightToken;

        /// <summary>
        /// The db provider factory.
        /// </summary>
        protected System.Data.Common.DbProviderFactory dbProviderFactory;
        /// <summary>
        /// The db connection string builder
        /// </summary>
        protected DbConnectionStringBuilder dbConnStrBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DbProvider"/> class.
        /// </summary>
        /// <param name="connectionString">The conn STR.</param>
        /// <param name="dbProviderFactory">The db provider factory.</param>
        /// <param name="leftToken">leftToken</param>
        /// <param name="paramPrefixToken">paramPrefixToken</param>
        /// <param name="rightToken">rightToken</param>
        protected DbProvider(string connectionString, DbProviderFactory dbProviderFactory, char leftToken, char rightToken, char paramPrefixToken)
        {
            dbConnStrBuilder = new DbConnectionStringBuilder();
            dbConnStrBuilder.ConnectionString = connectionString;
            this.dbProviderFactory = dbProviderFactory;
            this.leftToken = leftToken;
            this.rightToken = rightToken;
            this.paramPrefixToken = paramPrefixToken;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:DbProvider"/> class.
        /// </summary>
        /// <param name="connectionString">The conn STR.</param>
        /// <param name="dbProviderFactory">The db provider factory.</param>
        /// <param name="leftToken">leftToken</param>
        /// <param name="paramPrefixToken">paramPrefixToken</param>
        /// <param name="rightToken">rightToken</param>
        /// <param name="likeToken">likeToken</param>
        protected DbProvider(string connectionString, DbProviderFactory dbProviderFactory, char leftToken, char rightToken, char paramPrefixToken, char likeToken = '%')
        {
            dbConnStrBuilder = new DbConnectionStringBuilder();
            dbConnStrBuilder.ConnectionString = connectionString;
            this.dbProviderFactory = dbProviderFactory;
            this.leftToken = leftToken;
            this.rightToken = rightToken;
            this.paramPrefixToken = paramPrefixToken;
            this.likeToken = likeToken;
        }
        #endregion


        #region Properties
        /// <summary>
        /// 
        /// </summary>
        private DBType databaseType;
        /// <summary>
        /// ConnectionStrings 节点名称
        /// </summary>
        public DBType DatabaseType
        {
            get { return databaseType; }
            set { databaseType = value; }
        }

        private string connectionStringName;

        /// <summary>
        /// ConnectionStrings 节点名称
        /// </summary>
        public string ConnectionStringName
        {
            get { return connectionStringName; }
            set { connectionStringName = value; }
        }


        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>The connection string.</value>
        public string ConnectionString
        {
            get
            {
                return dbConnStrBuilder.ConnectionString;
            }
        }

        /// <summary>
        /// Gets the db provider factory.
        /// </summary>
        /// <value>The db provider factory.</value>
        public System.Data.Common.DbProviderFactory DbProviderFactory
        {
            get
            {
                return dbProviderFactory;
            }
        }

        /// <summary>
        /// Gets the param prefix.
        /// </summary>
        /// <value>The param prefix.</value>
        public char ParamPrefix { get { return paramPrefixToken; } }

        /// <summary>
        /// Gets the left token of table name or column name.
        /// </summary>
        /// <value>The left token.</value>
        public char LeftToken { get { return leftToken; } }

        /// <summary>
        /// Gets the right token of table name or column name.
        /// </summary>
        /// <value>The right token.</value>
        public char RightToken { get { return rightToken; } }

        #endregion




        /// <summary>
        /// 自增长字段查询语句
        /// </summary>
        public abstract string RowAutoID
        {
            get;
        }

        /// <summary>
        /// 是否支持批量sql提交
        /// </summary>
        public abstract bool SupportBatch
        {
            get;
        }

        /// <summary>
        /// Builds the name of the parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public virtual string BuildParameterName(string name)
        {
            string nameStr = name.Trim(leftToken, rightToken);
            if (nameStr[0] != paramPrefixToken)
            {
                if ("@?:".Contains(nameStr[0].ToString()))
                {
                    nameStr = nameStr.Substring(1).Insert(0, new string(paramPrefixToken, 1));
                }
                else
                {
                    nameStr = nameStr.Insert(0, new string(paramPrefixToken, 1));
                }
            }
            //剔除参数中的“.” 
            return nameStr.Replace(".", "_");
        }

        /// <summary>
        /// Builds the name of the table.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="userName">The name.</param>
        /// <returns></returns>
        public virtual string BuildTableName(string name, string userName)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "";
            }
            else
            {
                if (string.IsNullOrWhiteSpace(userName))
                {
                    return string.Concat(leftToken.ToString(), name.Trim(leftToken, rightToken), rightToken.ToString());
                }
                return string.Concat(leftToken.ToString(), userName.Trim(leftToken, rightToken), rightToken.ToString())
                    + "."
                    + string.Concat(leftToken.ToString(), name.Trim(leftToken, rightToken), rightToken.ToString());
            }
        }


        /// <summary>
        /// 调整DbCommand命令
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public virtual void PrepareCommand(DbCommand cmd)
        {
            bool isStoredProcedure = (cmd.CommandType == CommandType.StoredProcedure);

            foreach (DbParameter p in cmd.Parameters)
            {

                if (!isStoredProcedure)
                {
                    //TODO 这里可以继续优化
                    if (cmd.CommandText.IndexOf(p.ParameterName, StringComparison.Ordinal) == -1)
                    {
                        cmd.CommandText = cmd.CommandText.Replace("@" + p.ParameterName.Substring(1), p.ParameterName);
                        cmd.CommandText = cmd.CommandText.Replace("?" + p.ParameterName.Substring(1), p.ParameterName);
                        cmd.CommandText = cmd.CommandText.Replace(":" + p.ParameterName.Substring(1), p.ParameterName);
                        //if (p.ParameterName.Substring(0, 1) == "?" || p.ParameterName.Substring(0, 1) == ":"
                        //        || p.ParameterName.Substring(0, 1) == "@")
                        //    cmd.CommandText = cmd.CommandText.Replace(paramPrefixToken + p.ParameterName.Substring(1), p.ParameterName);
                        //else
                        //    cmd.CommandText = cmd.CommandText.Replace(p.ParameterName.Substring(1), p.ParameterName);
                    }
                }

                if (p.Direction == ParameterDirection.Output || p.Direction == ParameterDirection.ReturnValue)
                {
                    continue;
                }

                object value = p.Value;
                DbType dbType = p.DbType;

                if (value == DBNull.Value)
                {
                    continue;
                }

                if (value == null)
                {
                    p.Value = DBNull.Value;
                    continue;
                }

                Type type = value.GetType();

                if (type.IsEnum)
                {
                    p.DbType = DbType.Int32;
                    p.Value = Convert.ToInt32(value);
                    continue;
                }

                if (dbType == DbType.Guid && type != typeof(Guid))
                {
                    p.Value = new Guid(value.ToString());
                    continue;
                }
                //if ((dbType == DbType.AnsiString || dbType == DbType.String ||
                //    dbType == DbType.AnsiStringFixedLength || dbType == DbType.StringFixedLength) && (!(value is string)))
                //{
                //    p.Value = SerializationManager.Serialize(value);
                //    continue;
                //}

                if (type == typeof(Boolean))
                {
                    p.Value = (((bool)value) ? 1 : 0);
                    continue;
                }
            }
        }


        #region << 参数准备 >>

        /// <summary>
        /// 得到数据库SQL参数对象
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">参数值</param>
        /// <returns>返回SQL参数对象</returns>
        public virtual DbParameter GetDbParameter(string Name, object value)
        {
            DbParameter param = DbProviderFactory.CreateParameter();

            return param;
        }

        // <summary>
        /// 得到数据库SQL参数对象
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="dbType">数据类型</param>
        /// <returns>返回SQL参数对象</returns>
        public virtual DbParameter GetDbParameter(string parameterName, object value, DbType dbType)
        {
            return null;
        }
        #endregion

        #region <<基于实体增删改查方法>>

        /// <summary>
        /// 准备插入语句
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="insertSql"></param>
        /// <param name="parms"></param>
        public virtual SqlBuilder GetInsertSqlBuilder<T>(T entity)
        {
            try
            {
                EntityInfo entityInfo = Caches.EntityInfoCache.Get(typeof(T));
                StringBuilder insertSql = new StringBuilder();
                List<DbParameter> parms = new List<DbParameter>();
                insertSql.AppendFormat(" INSERT INTO {0} ( ", entityInfo.TableName);
                StringBuilder insertFields = new StringBuilder();
                StringBuilder insertValues = new StringBuilder();

                foreach (PropertyInfo property in entityInfo.ColumnProperties)
                {
                    object propertyValue = null;
                    if (!property.Name.Equals(entityInfo.AutoGeneratedKey) && (propertyValue = property.GetValue(entity, null)) != null)
                    {
                        insertFields.AppendFormat(" {0},", entityInfo.Columns[property.Name]);
                        insertValues.AppendFormat("{0}{1},", paramPrefixToken, property.Name);
                        parms.Add(GetDbParameter(property.Name, propertyValue));
                    }
                }
                //如果含有自增列
                if (!string.IsNullOrEmpty(entityInfo.AutoGeneratedKey))
                {
                    insertSql.AppendFormat(" {0}) VALUES ({1}) ", insertFields.ToString().TrimEnd(','), insertValues.ToString().TrimEnd(','));
                    insertSql.Append(RowAutoID);
                }
                else
                {
                    insertSql.AppendFormat(" {0}) VALUES ({1}) ", insertFields.ToString().TrimEnd(','), insertValues.ToString().TrimEnd(','));
                }

                return new SqlBuilder
                {
                    Sql = insertSql.ToString(),
                    DbParameters = parms
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// 准备删除语句
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="deleteSql"></param>
        /// <param name="parms"></param>
        public virtual SqlBuilder GetDeleteSqlBuilder<T>(T entity)
        {
            try
            {
                EntityInfo entityInfo = Caches.EntityInfoCache.Get(typeof(T));
                if (entityInfo.KeyProperties.Count == 0)
                {
                    throw new Exception("进行Delete操作时，实体没有声明主键特性");
                }
                StringBuilder deleteSql = new StringBuilder();
                List<DbParameter> parms = new List<DbParameter>();
                deleteSql.AppendFormat(" DELETE FROM {0} WHERE ", entityInfo.TableName);
                StringBuilder whereClause = new StringBuilder();
                foreach (PropertyInfo property in entityInfo.KeyProperties)
                {
                    object propertyValue = null;
                    if ((propertyValue = property.GetValue(entity, null)) != null)
                    {
                        whereClause.AppendFormat(" {0}={1}{2} AND", entityInfo.Columns[property.Name], paramPrefixToken, property.Name);
                        parms.Add(GetDbParameter(property.Name, propertyValue));
                    }
                }
                deleteSql.Append(whereClause.ToString().TrimEnd("AND".ToArray()));

                return new SqlBuilder
                {
                    Sql = deleteSql.ToString(),
                    DbParameters = parms
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据lambda表达式条件删除操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <returns></returns>
        public virtual SqlBuilder GetDeleteSqlBuilder<T>(Expression<Func<T, bool>> exp)
        {
            try
            {
                EntityInfo entityInfo = Caches.EntityInfoCache.Get(typeof(T));
                StringBuilder deleteSql = new StringBuilder();
                List<DbParameter> parms = new List<DbParameter>();
                deleteSql.AppendFormat(" DELETE FROM {0} WHERE ", entityInfo.TableName);

                if (exp != null)
                {
                    QueryResult result = DynamicQuery.GetDynamicWhere(exp);
                    deleteSql.Append(result.Sql);
                    foreach (var parm in result.Param)
                    {
                        parms.Add(GetDbParameter(parm.Key, parm.Value));
                    }
                }
                else
                {
                    throw new Exception("进行Delete操作时，lambda表达式为null");
                }

                return new SqlBuilder
                {
                    Sql = deleteSql.ToString(),
                    DbParameters = parms
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 更新sql语句
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="updateSql"></param>
        /// <param name="parms"></param>
        public virtual SqlBuilder GetUpdateSqlBuilder<T>(T entity)
        {
            try
            {
                EntityInfo entityInfo = Caches.EntityInfoCache.Get(typeof(T));
                if (entityInfo.PrimaryKey.Count == 0)
                {
                    throw new Exception("进行Delete操作时，实体没有声明主键特性");
                }
                StringBuilder updateSql = new StringBuilder();
                List<DbParameter> parms = new List<DbParameter>();
                updateSql.AppendFormat(" UPDATE {0} SET ", entityInfo.TableName);
                StringBuilder updateValues = new StringBuilder();
                StringBuilder whereClause = new StringBuilder();
                foreach (PropertyInfo property in entityInfo.NotKeyColumnProperties)
                {
                    object propertyValue = null;
                    if ((propertyValue = property.GetValue(entity, null)) != null)
                    {
                        updateValues.AppendFormat("{0}={1}{2},", entityInfo.Columns[property.Name], paramPrefixToken, property.Name);
                        parms.Add(GetDbParameter(property.Name, propertyValue));
                    }
                }
                updateSql.AppendFormat("{0} WHERE ", updateValues.ToString().TrimEnd(','));
                foreach (PropertyInfo property in entityInfo.KeyProperties)
                {
                    whereClause.AppendFormat(" {0}={1}{2} AND", entityInfo.Columns[property.Name], paramPrefixToken, property.Name);
                    parms.Add(GetDbParameter(property.Name, property.GetValue(entity, null)));
                }
                updateSql.Append(whereClause.ToString().TrimEnd("AND".ToArray()));

                return new SqlBuilder
                {
                    Sql = updateSql.ToString(),
                    DbParameters = parms
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据lambda表达式更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public virtual SqlBuilder GetUpdateSqlBuilder<T>(T entity, Expression<Func<T, bool>> exp)
        {
            try
            {
                EntityInfo entityInfo = Caches.EntityInfoCache.Get(typeof(T));
                StringBuilder updateSql = new StringBuilder();
                List<DbParameter> parms = new List<DbParameter>();
                updateSql.AppendFormat(" UPDATE {0} SET ", entityInfo.TableName);
                StringBuilder updateValues = new StringBuilder();
                StringBuilder whereClause = new StringBuilder();
                foreach (PropertyInfo property in entityInfo.NotKeyColumnProperties)
                {
                    object propertyValue = null;
                    if ((propertyValue = property.GetValue(entity, null)) != null)
                    {
                        updateValues.AppendFormat("{0}={1}{2},", entityInfo.Columns[property.Name], paramPrefixToken, property.Name);
                        parms.Add(GetDbParameter(property.Name, propertyValue));
                    }
                }
                updateSql.AppendFormat("{0} WHERE ", updateValues.ToString().TrimEnd(','));

                if (exp != null)
                {
                    QueryResult result = DynamicQuery.GetDynamicWhere(exp);
                    updateSql.Append(result.Sql);
                    foreach (var parm in result.Param)
                    {
                        parms.Add(GetDbParameter(parm.Key, parm.Value));
                    }
                }
                else
                {
                    throw new Exception("进行Update操作时，lambda表达式为null");
                }

                return new SqlBuilder
                {
                    Sql = updateSql.ToString(),
                    DbParameters = parms
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SqlBuilder GetUpdateSqlBuilder<T>(Expression<Func<T, T>> updateExp, Expression<Func<T, bool>> exp)
        {
            try
            {
                EntityInfo entityInfo = Caches.EntityInfoCache.Get(typeof(T));
                StringBuilder updateSql = new StringBuilder();
                List<DbParameter> parms = new List<DbParameter>();
                updateSql.AppendFormat(" UPDATE {0} SET ", entityInfo.TableName);
                /* SqlVisitor updateVisitor = new SqlVisitor(this.DataBase.DBType, 0, VisitorType.UpdateSet);
                 string updateSet = updateVisitor.Translate(updateExp);
                 foreach (DbParameter parm in updateVisitor.Parameters)
                 {
                     parms.Add(parm);
                 }
                 StringBuilder whereClause = new StringBuilder();
                 updateSql.AppendFormat("{0} WHERE ", updateSet.TrimEnd(','));

                 if (exp != null)
                 {
                     SqlVisitor lambdaTranslator = new SqlVisitor(this.DataBase.DBType, 1);
                     string where = lambdaTranslator.Translate(exp);
                     updateSql.Append(where);
                     foreach (DbParameter parm in lambdaTranslator.Parameters)
                     {
                         parms.Add(parm);
                     }
                     return DataBase.ExecuteSql(updateSql.ToString(), parms.ToArray());
                 }
                 else
                 {
                     throw new LambdaLossException("进行Update操作时，lambda表达式为null");
                 }*/
                return new SqlBuilder
                {
                    Sql = updateSql.ToString(),
                    DbParameters = parms
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  忽略指定字段更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="ignoreFields"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public virtual SqlBuilder GetUpdateIgnoreFieldsSqlBuilder<T>(T entity, Expression<Func<T, dynamic>> ignoreFields, Expression<Func<T, bool>> exp)
        {
            try
            {
                StringBuilder updateSql = new StringBuilder();
                List<DbParameter> parms = new List<DbParameter>();
                /*
                DynamicVisitor visitor = new DynamicVisitor();
                visitor.Translate(ignoreFields);
                List<string> ignores = visitor.DynamicMembers.Select(m => m.Key).ToList();
                EntityInfo entityInfo = Caches.EntityInfoCache.Get(typeof(T));                
                updateSql.AppendFormat(" UPDATE {0} SET ", entityInfo.TableName);
                StringBuilder updateValues = new StringBuilder();
                StringBuilder whereClause = new StringBuilder();
                foreach (PropertyInfo property in entityInfo.NotKeyColumnProperties)
                {
                    object propertyValue = null;
                    if ((propertyValue = property.GetValue(entity, null)) != null && !ignores.Contains(property.Name))
                    {
                        updateValues.AppendFormat("{0}={1}{2},", entityInfo.Columns[property.Name], paramPrefixToken, property.Name);
                        parms.Add(GetDbParameter(property.Name, propertyValue));
                    }
                }
                updateSql.AppendFormat("{0} WHERE ", updateValues.ToString().TrimEnd(','));
                if (exp != null)
                {
                    SqlVisitor lambdaTranslator = new SqlVisitor(this.DataBase.DBType);
                    string where = lambdaTranslator.Translate(exp);
                    updateSql.Append(where);
                    foreach (DbParameter parm in lambdaTranslator.Parameters)
                    {
                        parms.Add(parm);
                    }
                    return DataBase.ExecuteSql(updateSql.ToString(), parms.ToArray());
                }
                else
                {
                    throw new LambdaLossException("进行Update操作时，lambda表达式为null");
                }*/
                return new SqlBuilder
                {
                    Sql = updateSql.ToString(),
                    DbParameters = parms
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SqlBuilder GetUpdateOnlyFieldsSqlBuilder<T>(T entity, Expression<Func<T, dynamic>> onlyFields, Expression<Func<T, bool>> exp)
        {
            /* DynamicVisitor visitor = new DynamicVisitor();
             visitor.Translate(onlyFields);
             List<string> onlys = visitor.DynamicMembers.Select(m => m.Key).ToList();
             return UpdateT<T>(entity, onlys, exp);*/

            return null;

        }

        /// <summary>
        /// 根据指定字段更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="updateFields"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public virtual SqlBuilder GetUpdateSqlBuilder<T>(T entity, List<string> updateFields, Expression<Func<T, bool>> exp)
        {
            try
            {
                EntityInfo entityInfo = Caches.EntityInfoCache.Get(typeof(T));
                StringBuilder updateSql = new StringBuilder();
                List<DbParameter> parms = new List<DbParameter>();
                updateSql.AppendFormat(" UPDATE {0} SET ", entityInfo.TableName);
                StringBuilder updateValues = new StringBuilder();
                StringBuilder whereClause = new StringBuilder();
                foreach (PropertyInfo property in entityInfo.ColumnProperties.Where(m => updateFields.Contains(m.Name)))
                {
                    object propertyValue = null;
                    if ((propertyValue = property.GetValue(entity, null)) != null)
                    {
                        updateValues.AppendFormat("{0}={1}{2},", entityInfo.Columns[property.Name], paramPrefixToken, property.Name);
                        parms.Add(GetDbParameter(property.Name, propertyValue));
                    }
                    else
                    {
                        updateValues.Append(entityInfo.Columns[property.Name] + "=null,");
                    }
                }
                updateSql.AppendFormat("{0} WHERE ", updateValues.ToString().TrimEnd(','));


                if (exp != null)
                {
                    QueryResult result = DynamicQuery.GetDynamicWhere(exp);
                    updateSql.Append(result.Sql);
                    foreach (var parm in result.Param)
                    {
                        parms.Add(GetDbParameter(parm.Key, parm.Value));
                    }
                }
                else
                {
                    throw new Exception("进行Update操作时，lambda表达式为null");
                }
                return new SqlBuilder
                {
                    Sql = updateSql.ToString(),
                    DbParameters = parms
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public SqlBuilder GetSQLByLambda<T>(Expression<Func<T, bool>> exp)
        {
            StringBuilder selectSql = new StringBuilder();
            List<DbParameter> parms = new List<DbParameter>();
            EntityInfo entityInfo = Caches.EntityInfoCache.Get(typeof(T));
            selectSql.AppendFormat("SELECT {0} FROM {1}", entityInfo.SelectFields, entityInfo.TableName);
            if (exp != null)
            {
                QueryResult result = DynamicQuery.GetDynamicWhere(exp);
                selectSql.AppendFormat(" WHERE {0}", result.Sql);
                foreach (var parm in result.Param)
                {
                    parms.Add(GetDbParameter(parm.Key, parm.Value));
                }
            }
            return new SqlBuilder
            {
                Sql = selectSql.ToString(),
                DbParameters = parms
            };
        }

        /// <summary>
        /// 翻译ExistsSQL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selectSql"></param>
        /// <param name="parms"></param>
        /// <param name="exp"></param>
        public SqlBuilder GetExistsSQLByLambda<T>(Expression<Func<T, bool>> exp) where T : class, new()
        {
            StringBuilder selectSql = new StringBuilder();
            List<DbParameter> parms = new List<DbParameter>();
            EntityInfo entityInfo = Caches.EntityInfoCache.Get(typeof(T));
            selectSql.AppendFormat("SELECT COUNT(1) CT FROM {0}", entityInfo.TableName);
            if (exp != null)
            {
                QueryResult result = DynamicQuery.GetDynamicWhere(exp);
                selectSql.AppendFormat(" WHERE {0}", result.Sql);
                foreach (var parm in result.Param)
                {
                    parms.Add(GetDbParameter(parm.Key, parm.Value));
                }
            }
            return new SqlBuilder
            {
                Sql = selectSql.ToString(),
                DbParameters = parms
            };
        }
        #endregion

        #region<<反射辅助方法>>

        private BindingFlags BindFlag = BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance;
        public void SetEntityMembers<T>(IDataReader reader, T entity)
        {
            int count = reader.FieldCount;

            for (int i = 0; i < count; i++)
            {
                PropertyInfo property;
                if (reader[i] != DBNull.Value)
                {
                    //如果列名=实体字段名
                    if ((property = typeof(T).GetProperty(reader.GetName(i), BindFlag)) != null)
                    {
                        IPropertyAccessor propertyAccessor = Caches.PropertyAccessorCache.Get(property);
                        propertyAccessor.SetValue(entity, reader[i]);
                    }
                    else
                    {
                        //如果列名!=实体字段名
                        string propertyname = Caches.EntityInfoCache.Get(typeof(T)).Columns.FirstOrDefault(m => m.Value == reader.GetName(i)).Key;
                        if (!string.IsNullOrEmpty(propertyname) && (property = typeof(T).GetProperty(propertyname, BindFlag)) != null)
                        {
                            IPropertyAccessor propertyAccessor = Caches.PropertyAccessorCache.Get(property);
                            propertyAccessor.SetValue(entity, reader[i]);
                        }
                    }
                }
            }
        }

        public void SetDictionaryValues(IDataReader reader, Dictionary<string, object> dic)
        {
            int count = reader.FieldCount;
            for (int i = 0; i < count; i++)
            {
                if (reader[i] != DBNull.Value)
                {
                    dic.Add(reader.GetName(i), reader[i]);
                }
                else
                {
                    dic.Add(reader.GetName(i), null);
                }
            }
        }

        /*
        public TObject GetDynamicObject<TObject>(IDataReader reader)
        {
            //为什么不能根据reader列数判断 因为实体存在一个属性的实体
            if (!typeof(TObject).IsClass || typeof(TObject) == typeof(string))
            {
                if (reader[0] != DBNull.Value)
                {
                    if (typeof(TObject) == reader[0].GetType())
                    {
                        return (TObject)reader[0];
                    }
                    else
                    {
                        return (TObject)Convert.ChangeType(reader[0], typeof(TObject));
                    }
                }
                else
                {
                    return default(TObject);
                }
            }
            else
            {
                //dynamic
                if (typeof(TObject) == typeof(object))
                {
                    var dict = new Dictionary<string, object>();
                    SetDictionaryValues(reader, dict);
                    if (dict.Count() == 1)
                    {
                        return (TObject)dict.FirstOrDefault().Value;
                    }
                    var eo = new System.Dynamic.ExpandoObject();
                    var eoColl = (ICollection<KeyValuePair<string, object>>)eo;
                    foreach (var kvp in dict)
                    {
                        eoColl.Add(kvp);
                    }
                    dynamic eoDynamic = eo;
                    return eoDynamic;
                }
                else
                {
                    TObject entity = ((Func<TObject>)Caches.ConstructorCache.Get(typeof(TObject)))();
                    SetEntityMembers<TObject>(reader, entity);
                    return entity;
                }
            }
        }
        */

        /// <summary>
        /// 常规反射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
        /// <param name="properties"></param>
        public void SetEntityMembers1<T>(IDataReader reader, T entity)
        {
            int count = reader.FieldCount;
            for (int i = 0; i < count; i++)
            {
                PropertyInfo property;
                if (reader[i] != DBNull.Value && (property = typeof(T).GetProperty(reader.GetName(i), BindFlag)) != null)
                {
                    if (!property.PropertyType.IsGenericType)
                    {
                        property.SetValue(entity, Convert.ChangeType(reader[property.Name], property.PropertyType), null);
                    }
                    else
                    {
                        Type genericTypeDefinition = property.PropertyType.GetGenericTypeDefinition();
                        if (genericTypeDefinition == typeof(Nullable<>))
                        {
                            property.SetValue(entity, Convert.ChangeType(reader[property.Name], Nullable.GetUnderlyingType(property.PropertyType)), null);
                        }
                    }
                }
            }
        }


        #region DataTable转List<T>
        /// <summary>
        /// DataTable转List<T>
        /// </summary>
        /// <typeparam name="T">数据项类型</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns>List数据集</returns>
        public List<T> DataTableToList<T>(DataTable dt) where T : new()
        {
            List<T> tList = new List<T>();
            if (dt == null || dt.Rows.Count == 0)
            {
                return tList;
            }
            PropertyInfo[] propertys = typeof(T).GetProperties();   //获取此实体的公共属性
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                foreach (PropertyInfo pi in propertys)
                {
                    if (!pi.CanWrite)
                    {
                        continue;
                    }
                    string columnName = pi.Name;
                    if (dr.Table.Columns.Contains(columnName))
                    {
                        // 判断此属性是否有Setter或columnName值是否为空
                        object value = dr[columnName];
                        if (value is DBNull || value == DBNull.Value || value == null || !pi.CanWrite)
                        {
                            continue;
                        }

                        #region SetValue
                        try
                        {
                            string propertyType = pi.PropertyType.ToString();
                            if (propertyType.StartsWith("System.Nullable`1"))
                            {
                                propertyType = pi.PropertyType.ToString().Replace("System.Nullable`1", "").TrimStart('[').TrimEnd(']');
                            }
                            switch (propertyType)
                            {
                                case "System.String":
                                    pi.SetValue(t, Convert.ToString(value), null);
                                    break;
                                case "System.ToChar":
                                    pi.SetValue(t, Convert.ToChar(value), null);
                                    break;
                                case "System.Int64":
                                    pi.SetValue(t, Convert.ToInt64(value), null);
                                    break;
                                case "System.Int32":
                                    pi.SetValue(t, Convert.ToInt32(value), null);
                                    break;
                                case "System.ToUInt64":
                                    pi.SetValue(t, Convert.ToUInt64(value), null);
                                    break;
                                case "System.ToUInt32":
                                    pi.SetValue(t, Convert.ToUInt32(value), null);
                                    break;
                                case "System.DateTime":
                                    pi.SetValue(t, Convert.ToDateTime(value), null);
                                    break;
                                case "System.Boolean":
                                    pi.SetValue(t, Convert.ToBoolean(value), null);
                                    break;
                                case "System.Double":
                                    pi.SetValue(t, Convert.ToDouble(value), null);
                                    break;
                                case "System.Decimal":
                                    pi.SetValue(t, Convert.ToDecimal(value), null);
                                    break;
                                case "System.Single":
                                    pi.SetValue(t, Convert.ToSingle(value), null);
                                    break;
                                default:
                                    pi.SetValue(t, value, null);
                                    break;
                            }
                        }
                        catch
                        {
                            //throw (new Exception(ex.Message));
                        }
                        #endregion
                    }
                }
                tList.Add(t);
            }
            return tList;
        }
        #endregion

        #endregion

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
