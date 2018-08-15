using Dotnet.NHibernate;
using Dotnet.Data;
using Dotnet.Data.Providers;
using Dotnet.Dependency;
using System.Configuration;

namespace DotCommon.Configurations
{
    public static class ConfigurationExtensions
    {
        /// <summary>使用Dapper容器
        /// </summary>
        public static Configuration UseDapper(this Configuration configuration)
        {
            var container = IocManager.GetContainer();
            
            container.Register(typeof(RepositoryBase<,>),typeof(NHibernateRepositoryBase<,>),DependencyLifeStyle.Transient);
            container.Register(typeof(IRepository<,>), typeof(NHibernateRepositoryBase<,>), DependencyLifeStyle.Transient);

            container.Register<NHibernateActiveTransactionProvider, NHibernateActiveTransactionProvider>(DependencyLifeStyle.Singleton);

            return configuration;
        }

    }
}
