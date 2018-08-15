using Dotnet.Common.Http;
using Dotnet.Mvc.Entity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Dotnet.Mvc.Filters {
    /// <summary>
    /// 异常处理过滤器
    /// </summary>
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute {
        /// <summary>
        /// 异常处理
        /// </summary>
        public override void OnException( ExceptionContext context ) {
            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = 200;
            context.Result = new MvcResult( StateCode.Fail, context.Exception.Message  );
        }
    }
}
