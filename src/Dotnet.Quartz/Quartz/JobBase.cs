using Dotnet.Logging;
using Quartz;
using System.Threading.Tasks;

namespace Dotnet.Quartz
{
    public abstract class JobBase : IJob
    {
        public ILogger Logger { get; set; }
        protected JobBase()
        {
        }

        public abstract Task Execute(IJobExecutionContext context);

    }
}
