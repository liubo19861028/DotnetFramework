using Castle.Windsor;
using Dotnet.CastleWindsor;
using Dotnet.Dependency;

namespace Dotnet.Configurations
{
    public static class ConfigurationExtension
    {
        /// <summary>使用CastleWindsor
        /// </summary>
        public static Configuration UseCastleWindsor(this Configuration configuration, IWindsorContainer container)
        {
            var iocContainer = new CastleWindsorIocContainer(container);
            IocManager.SetContainer(iocContainer);
            return configuration;
        }
    }
}
