using Dotnet.Dependency;
using Dotnet.Extensions;
using Quartz;
using Quartz.Spi;

namespace Dotnet.Quartz
{
    public class DotnetQuartzJobFactory : IJobFactory
    {

        public DotnetQuartzJobFactory()
        {
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return IocManager.GetContainer().Resolve(bundle.JobDetail.JobType).As<IJob>();
        }

        public void ReturnJob(IJob job)
        {
            //释放
            //_iocResolver.Release(job);
        }
    }
}
