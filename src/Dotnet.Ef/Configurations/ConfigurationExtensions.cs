using Dotnet.Data;
using Dotnet.Data.Providers;
using Dotnet.Dependency;
using Dotnet.Ef;

namespace Dotnet.Configurations
{
    public static class ConfigurationExtensions
    {
        /// <summary>使用Dapper容器
        /// </summary>
        public static Configuration UseEf(this Configuration configuration)
        {
            var container = IocManager.GetContainer();
            
            container.Register(typeof(RepositoryBase<,>),typeof(EfRepositoryBase<,>),DependencyLifeStyle.Transient);
            container.Register(typeof(IRepository<,>), typeof(EfRepositoryBase<,>), DependencyLifeStyle.Transient);
            container.Register<EfActiveTransactionProvider, EfActiveTransactionProvider>(DependencyLifeStyle.Singleton);

            //container.Register(typeof(RepositoryBase<>), typeof(EfRepositoryBase<>), DependencyLifeStyle.Transient);
            return configuration;
        }

    }
}
