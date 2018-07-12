using Autofac;
using Autofac.Integration.WebApi;
using DotCommon.Dependency;
using DotCommon.Logging;
using DotCommon.Services;
using Microsoft.Owin.Hosting;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DotCommon.Host
{
    internal sealed class DotCommonServices : DotCommon.Services.Service
    {

        private const string SearchPath = "services";
        private static List<IService> microServices = new List<IService>();
        public ILogger Logger { get; }

        public DotCommonServices()
        {
            Logger = IocManager.GetContainer().Resolve<ILoggerFactory>().Create(DotCommonConsts.LoggerName);
        }

        private void DiscoverServices(ContainerBuilder builder)
        {
            var searchFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), SearchPath);
            foreach (var file in Directory.EnumerateFiles(searchFolder, "*.dll", SearchOption.AllDirectories))
            {
                try
                {
                    var assembly = Assembly.LoadFrom(file);
                    var exportedTypes = assembly.GetExportedTypes();
                   /* var microserviceRegisterType = exportedTypes.FirstOrDefault(x => x.IsSubclassOf(typeof(Autofac.Module)) &&
                        x.BaseType.GetGenericTypeDefinition() == typeof(MicroserviceRegister<>));
                    if (microserviceRegisterType != null)
                    {
                        var registeringService = microserviceRegisterType.BaseType.GetGenericArguments().First();
                        if (configuration.Services.All(x => x.Type != registeringService.FullName))
                        {
                            continue;
                        }
                        var mod = (Autofac.Module)Activator.CreateInstance(microserviceRegisterType, configuration);
                        builder.RegisterModule(mod);
                    }*/

                    if (exportedTypes.Any(t => t.IsSubclassOf(typeof(ApiController))))
                    {
                        builder.RegisterApiControllers(assembly).InstancePerRequest();
                    }
                }
                catch { }
            }
        }

        public void Configuration(IAppBuilder app)
        {

            var builder = new ContainerBuilder();

            // Discovers the services.
            DiscoverServices(builder);

            // Register the API controllers within the current assembly.
            builder.RegisterApiControllers(this.GetType().Assembly);

            // Create the container by builder.
            var container = builder.Build();

            // Register the services.
            microServices.AddRange(container.Resolve<IEnumerable<IService>>());

            app.UseAutofacMiddleware(container);

            HttpConfiguration config = new HttpConfiguration();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }

        public override void Start(object[] args)
        {
            var url =   ConfigurationManager.AppSettings["ApplicationSetting_Url"];

            Logger.Info("Starting WeText Service...");
            using (WebApp.Start<DotCommonServices>(url: url))
            {
                microServices.ForEach(ms =>
                {
                    Logger.Info($"Starting microservice '{ms.GetType().FullName}'...");
                    ms.Start(args);
                });
                Logger.Info("WeText Service started successfully.");
                Console.ReadLine();
            }
        }
    }
}
