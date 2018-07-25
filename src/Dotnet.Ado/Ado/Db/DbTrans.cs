using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Dotnet.Ado.Db
{
    /// <summary>
    /// 事务
    /// </summary>
    public class DbTrans : IDisposable
    {

        /// <summary>
        /// 事务
        /// </summary>
        private DbTransaction trans;

        /// <summary>
        /// 连接
        /// </summary>
        private DbConnection conn;


        /// <summary>
        /// 
        /// </summary>
        private Database db;

        /// <summary>
        /// 判断是否有提交或回滚
        /// </summary>
        private bool isCommitOrRollback = false;

        /// <summary>
        /// 是否关闭
        /// </summary>
        private bool isClose = false;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="dbSession"></param>
        public DbTrans(DbTransaction trans, Database db)
        {

            this.trans = trans;
            this.conn = trans.Connection;
            this.db = db;

            if (this.conn.State != ConnectionState.Open)
                this.conn.Open();

        }



        /// <summary>
        /// 连接
        /// </summary>
        public DbConnection Connection
        {
            get
            {
                return conn;
            }
        }

        /// <summary>
        /// 事务级别
        /// </summary>
        public IsolationLevel IsolationLevel
        {
            get { return trans.IsolationLevel; }
        }

        /// <summary>
        /// 提交
        /// </summary>
        public void Commit()
        {
            trans.Commit();

            isCommitOrRollback = true;

            Close();
        }


        /// <summary>
        /// 回滚
        /// </summary>
        public void Rollback()
        {
            trans.Rollback();

            isCommitOrRollback = true;

            Close();
        }


        /// <summary>
        /// 隐式转换
        /// </summary>
        /// <param name="dbTrans"></param>
        /// <returns></returns>
        public static implicit operator DbTransaction(DbTrans dbTrans)
        {
            return dbTrans.trans;
        }


        /// <summary>
        /// 关闭
        /// </summary>
        public void Close()
        {
            if (isClose)
                return;

            if (!isCommitOrRollback)
            {
                isCommitOrRollback = true;

                trans.Rollback();
            }

            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }

            trans.Dispose();

            isClose = true;
        }


        #region IDisposable 成员
        /// <summary>
        /// 关闭连接并释放资源
        /// </summary>
        public void Dispose()
        {
            Close();
        }

        #endregion


        
    }
}
