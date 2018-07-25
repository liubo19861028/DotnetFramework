using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using Dotnet.Ado.Common;
using MySql.Data.MySqlClient;

namespace Dotnet.Ado.Provider
{

    /// <summary>
    /// SqlServer 
    /// </summary>
    public class MySqlProvider : DbProvider
    {
        public MySqlProvider(string connectionString)
            : this(connectionString, SqlClientFactory.Instance)
        {
        }

        public MySqlProvider(string connectionString, DbProviderFactory dbFactory)
            : base(connectionString, dbFactory, '[', ']', '@')
        {
        }

        public override string RowAutoID
        {
            get { return "select scope_identity()"; }
        }

        public override bool SupportBatch
        {
            get { return true; }
        }

        /// <summary>
        /// Builds the name of the parameter.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public override string BuildParameterName(string name)
        {
            string nameStr = name.Trim(leftToken, rightToken);
            if (nameStr[0] != paramPrefixToken)
            {
                return nameStr.Insert(0, new string(paramPrefixToken, 1));
            }
            //剔除参数中的“.” 
            return nameStr.Replace(".", "_");
        }


        public override void PrepareCommand(DbCommand cmd)
        {
            base.PrepareCommand(cmd);

            foreach (DbParameter p in cmd.Parameters)
            {
                if (p.Direction == ParameterDirection.Output || p.Direction == ParameterDirection.ReturnValue)
                {
                    continue;
                }

                object value = p.Value;
                if (value == DBNull.Value)
                {
                    continue;
                }
                Type type = value.GetType();
                SqlParameter sqlParam = (SqlParameter)p;

                if (type == typeof(Guid))
                {
                    sqlParam.SqlDbType = SqlDbType.UniqueIdentifier;
                    continue;
                }

                switch (p.DbType)
                {
                    case DbType.Binary:
                        if (((byte[])value).Length > 8000)
                        {
                            sqlParam.SqlDbType = SqlDbType.Image;
                        }
                        break;
                    case DbType.Time:
                        sqlParam.SqlDbType = SqlDbType.DateTime;
                        break;
                    case DbType.DateTime:
                        sqlParam.SqlDbType = SqlDbType.DateTime;
                        break;
                    case DbType.AnsiString:
                        if (value.ToString().Length > 8000)
                        {
                            sqlParam.SqlDbType = SqlDbType.Text;
                        }
                        break;
                    case DbType.String:
                        if (value.ToString().Length > 4000)
                        {
                            sqlParam.SqlDbType = SqlDbType.NText;
                        }
                        break;
                    case DbType.Object:
                        sqlParam.SqlDbType = SqlDbType.NText;
                        p.Value = SerializationManager.Serialize(value);
                        break;
                }

                if (sqlParam.SqlDbType == SqlDbType.DateTime && type == typeof(TimeSpan))
                {
                    sqlParam.Value = new DateTime(1900, 1, 1).Add((TimeSpan)value);
                    continue;
                }
            }

            

           

        }


        /// <summary>
        /// 得到数据库SQL参数对象
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">参数值</param>
        /// <returns>返回SQL参数对象</returns>
        public override DbParameter GetDbParameter(string parameterName, object value)
        {
            return new MySqlParameter(paramPrefixToken + parameterName, value);
        }

        /// <summary>
        /// 得到数据库SQL参数对象
        /// </summary>
        /// <param name="parameterName">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="dbType">数据类型</param>
        /// <returns>返回SQL参数对象</returns>
        public override DbParameter GetDbParameter(string parameterName, object value, DbType dbType)
        {
            MySqlParameter parameter = new MySqlParameter(paramPrefixToken + parameterName, value);
            parameter.DbType = dbType;
            return parameter;
        }



    }
}
