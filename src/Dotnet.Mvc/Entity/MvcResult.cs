using Dotnet.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.Mvc.Entity
{
    /// <summary>
    /// 状态码
    /// </summary>
    public enum StateCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Ok = 1,
        /// <summary>
        /// 失败
        /// </summary>
        Fail = 2
    }

    /// <summary>
    /// 返回结果
    /// </summary>
    public class MvcResult : JsonResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        private readonly StateCode _code;
        /// <summary>
        /// 消息
        /// </summary>
        private readonly string _message;
        /// <summary>
        /// 数据
        /// </summary>
        private readonly dynamic _data;

        /// <summary>
        /// 初始化返回结果
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        public MvcResult(StateCode code, string message, dynamic data = null) : base(null)
        {
            _code = code;
            _message = message;
            _data = data;
        }

        /// <summary>
        /// 执行结果
        /// </summary>
        public override Task ExecuteResultAsync(ActionContext context)
        {
            this.Value = new
            {
                Code =EnumUtil.ToStr(_code),
                Message = _message,
                Data = _data
            };
            return base.ExecuteResultAsync(context);
        }
    }
}
