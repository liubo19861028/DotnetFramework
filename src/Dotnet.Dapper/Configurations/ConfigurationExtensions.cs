using Dotnet.Dapper;
using Dotnet.Dapper.Dapper;
using Dotnet.Data;
using Dotnet.Data.Providers;
using Dotnet.Dependency;

namespace Dotnet.Configurations
{
    public static class ConfigurationExtensions
    {
        /// <summary>使用Dapper容器
        /// </summary>
        public static Configuration UseDapper(this Configuration configuration)
        {
            var container = IocManager.GetContainer();

            container.Register(typeof(RepositoryBase<,>),typeof(DapperRepositoryBase<,>),DependencyLifeStyle.Transient);            
            container.Register<IActiveTransactionProvider, DapperActiveTransactionProvider>(DependencyLifeStyle.Transient);
            container.Register(typeof(IRepository<,>), typeof(DapperRepositoryBase<,>), DependencyLifeStyle.Transient);


           // container.Register(typeof(IRepository<>), typeof(DapperRepositoryBase<>), DependencyLifeStyle.Transient);
            return configuration;
        }

    }
}
