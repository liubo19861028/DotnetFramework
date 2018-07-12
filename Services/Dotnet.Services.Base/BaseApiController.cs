using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Web.Http;
using Dotnet.Logging;
using Dotnet.Dependency;
using Dotnet;

namespace Dotnet.Services.Base
{
    public class BaseApiController : ApiController
    {
        public ILogger Logger { get; set; }
        protected BaseApiController()
        {
            Logger = IocManager.GetContainer().Resolve<ILoggerFactory>().Create(DotnetConsts.LoggerName);
            Init();
        }

        public virtual void Init(){}
    }
}
