﻿using Autofac;
using Dotnet.Autofac;
using Dotnet.Data;
using Dotnet.Dependency;

namespace Dotnet.Configurations
{
    public static class ConfigurationExtensions
    {
        /// <summary>使用Autofac容器
        /// </summary>
        public static Configuration UseAutofac(this Configuration configuration, ContainerBuilder builder)
        {
            var container = new AutofacIocContainer(builder);
            IocManager.SetContainer(container);
            return configuration;
        }


        /// <summary>容器生效
        /// </summary>
        public static Configuration AutofacBuild(this Configuration configuration, IContainer container)
        {
            IocManager.GetContainer().UseEngine(container);
            return configuration;
        }

        /// <summary>容器生效
        /// </summary>
        public static Configuration AutofacBuild(this Configuration configuration)
        {
            var container = (AutofacIocContainer)IocManager.GetContainer();

            container.Build();
            return configuration;
        }

        /// <summary>容器生效
        /// </summary>
        public static Configuration AutofacBuild(this Configuration configuration,ref IContainer refContainer)
        {
            var container = (AutofacIocContainer)IocManager.GetContainer();

             container.Build();
             refContainer= container._container;
            return configuration;
        }
    }
}
