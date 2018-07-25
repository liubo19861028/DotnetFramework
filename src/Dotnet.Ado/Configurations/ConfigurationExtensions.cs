using Dotnet.Ado;
using Dotnet.Ado.Db;
using Dotnet.Data;
using Dotnet.Data.Providers;
using Dotnet.Dependency;

namespace Dotnet.Configurations
{
    public static class ConfigurationExtensions
    {
        /// <summary>使用Dapper容器
        /// </summary>
        public static Configuration UseAdo(this Configuration configuration)
        {
            var container = IocManager.GetContainer();

            container.Register(typeof(RepositoryBase<,>),typeof(AdoRepositoryBase<,>),DependencyLifeStyle.Transient);   
            container.Register(typeof(IRepository<,>), typeof(AdoRepositoryBase<,>), DependencyLifeStyle.Transient);
            container.Register<IActiveTransactionProvider, AdoActiveTransactionProvider>(DependencyLifeStyle.Transient);
            container.Register(typeof(Database), typeof(Database), DependencyLifeStyle.Singleton);
            return configuration;
        }

    }
}
