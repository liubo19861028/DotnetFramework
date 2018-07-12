using Dotnet.Dependency;
using Dotnet.Quartz;
using Dotnet.Quartz.Configuration;
using Quartz;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dotnet.Configurations
{
    public static class ConfigurationExtensions
    {

        /// <summary>Quartz
        /// </summary>
        public static Configuration UseQuartz(this Configuration configuration)
        {
            var container = IocManager.GetContainer();
            container.Register<IJobListener, DotnetQuartzJobListener>();
            //Quartz配置
            var quartzConfiguration = configuration.Startup.GetOrCreate<DotnetQuartzConfiguration>(nameof(DotnetQuartzConfiguration), () => new DotnetQuartzConfiguration());
            quartzConfiguration.InitScheduler();
            quartzConfiguration.Scheduler.JobFactory = new DotnetQuartzJobFactory();
            //Quartz配置
            container.Register<IQuartzScheduleJobManager, QuartzScheduleJobManager>();
            //Quartz管理,属于定时类型
            configuration.Startup.BackgroundWorker.AddWorkerType(typeof(IQuartzScheduleJobManager));
            return configuration;
        }

        /// <summary>注册Quartz Jobs
        /// </summary>
        public static Configuration RegisterQuartzJobs(this Configuration configuration, List<Assembly> assemblies)
        {
            var container = IocManager.GetContainer();
            var allTypies = assemblies.SelectMany(x => x.GetTypes());
            foreach (var type in allTypies)
            {
                if (typeof(JobBase).IsAssignableFrom(type) && !type.GetTypeInfo().IsAbstract)
                {
                    container.Register(type, DependencyLifeStyle.Transient);
                }
            }
            return configuration;
        }


        /// <summary>添加Quartz监听(在容器注册完成后)
        /// </summary>
        public static Configuration AddQuartzListener(this Configuration configuration)
        {
            var container = IocManager.GetContainer();
            configuration.Startup.Get<DotnetQuartzConfiguration>(nameof(DotnetQuartzConfiguration)).Scheduler.ListenerManager.AddJobListener(container.Resolve<IJobListener>());
            return configuration;
        }



    }
}
