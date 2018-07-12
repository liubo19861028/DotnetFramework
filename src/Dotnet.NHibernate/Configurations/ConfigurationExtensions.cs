using DotCommon.Dapper;
using DotCommon.Dapper.Dapper;
using DotCommon.Data;
using DotCommon.Data.Providers;
using DotCommon.Dependency;

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
            //container.Register(typeof(IDapperRepository<>), typeof(DapperRepositoryBase<,>), DependencyLifeStyle.Transient);

            container.Register<IActiveTransactionProvider, DapperActiveTransactionProvider>(DependencyLifeStyle.Transient);

            return configuration;
        }

    }
}
