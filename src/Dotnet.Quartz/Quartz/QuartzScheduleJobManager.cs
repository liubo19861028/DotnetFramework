using Dotnet.Quartz.Configuration;
using Dotnet.Threading.BackgroundWorkers;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Dotnet.Quartz
{
    public class QuartzScheduleJobManager : BackgroundWorkerBase, IQuartzScheduleJobManager
    {
        private readonly DotnetQuartzConfiguration _quartzConfiguration;
        public QuartzScheduleJobManager()
        {
            _quartzConfiguration = Dotnet.Configurations.Configuration.Instance.Startup.Get<DotnetQuartzConfiguration>(nameof(DotnetQuartzConfiguration));
        }


        public Task ScheduleAsync<TJob>(Action<JobBuilder> configureJob, Action<TriggerBuilder> configureTrigger) where TJob : IJob
        {
            var jobToBuild = JobBuilder.Create<TJob>();
            configureJob(jobToBuild);
            var job = jobToBuild.Build();

            var triggerToBuild = TriggerBuilder.Create();
            configureTrigger(triggerToBuild);
            var trigger = triggerToBuild.Build();

            _quartzConfiguration.Scheduler.ScheduleJob(job, trigger);

            return Task.FromResult(0);
        }


        public override void Start()
        {
            base.Start();
            _quartzConfiguration.Scheduler.Start();
            if (_quartzConfiguration.IsJobExecutionEnabled)
            {
                _quartzConfiguration.Scheduler.Start();
            }
            Logger.Info("Started QuartzScheduleJobManager");
        }

        public override void WaitToStop()
        {
            if (_quartzConfiguration.Scheduler != null)
            {
                try
                {
                    _quartzConfiguration.Scheduler.Shutdown(true);
                }
                catch (Exception ex)
                {
                    Logger.Warn(ex.ToString(), ex);
                }
            }

            base.WaitToStop();

            Logger.Info("Stopped QuartzScheduleJobManager");
        }
    }
}
