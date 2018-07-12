using Quartz;
using Quartz.Impl;

namespace Dotnet.Quartz.Configuration
{
    public class DotnetQuartzConfiguration
    {
        public DotnetQuartzConfiguration()
        {

        }
        public IScheduler Scheduler { get; private set; }

        public async void InitScheduler() => Scheduler = await StdSchedulerFactory.GetDefaultScheduler();

        /// <summary>是否启用Job定时
        /// </summary>
        public bool IsJobExecutionEnabled { get; set; } = true;


    }
}
