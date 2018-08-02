using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Dotnet.Data.Providers
{
    /// <summary>
    /// 参数
    /// </summary>
    [Serializable]
    public class Parameter
    {
        private string parameterName;
        private object parameterValue;
        private DbType? parameterDbType;
        private int? parameterSize;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        public Parameter(string parameterName, object parameterValue) : this(parameterName, parameterValue, null, null) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="parameterDbType"></param>
        /// <param name="parameterSize"></param>
        public Parameter(string parameterName, object parameterValue, DbType? parameterDbType, int? parameterSize)
        {
            this.parameterName = parameterName;
            this.parameterValue = parameterValue;
            this.parameterDbType = parameterDbType;
            this.parameterSize = parameterSize;
        }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParameterName
        {
            get { return parameterName; }
            set { parameterName = value; }
        }


        /// <summary>
        /// 参数值
        /// </summary>
        public object ParameterValue
        {
            get { return parameterValue; }
            set { parameterValue = value; }
        }

        /// <summary>
        /// 类型
        /// </summary>
        public DbType? ParameterDbType
        {
            get { return parameterDbType; }
            set { parameterDbType = value; }
        }

        /// <summary>
        /// 长度
        /// </summary>
        public int? ParameterSize
        {
            get { return parameterSize; }
            set { parameterSize = value; }
        }

    }

}
