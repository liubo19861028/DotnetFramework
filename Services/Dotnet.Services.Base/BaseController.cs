using Dotnet.Dependency;
using Dotnet.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace Dotnet.Services.Base
{
    public class BaseController : Controller
    {
        public ILogger Logger { get; set; }
        protected BaseController()
        {
            Logger = IocManager.GetContainer().Resolve<ILoggerFactory>().Create(DotnetConsts.LoggerName);
            Init();
        }

        public virtual void Init() { }
    }
}
