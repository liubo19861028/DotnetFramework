using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dotnet.Data.Providers;
using Dotnet.Utility;
using Dotnet.DapperTest;
using Dotnet.Dependency;
using Autofac;
using Dotnet.Configurations;
using System.Reflection;
using Dotnet.Dapper;
using Dotnet.Data;

namespace Dotnet.DapperTest1
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DapperDbContext>().As<IDbContext>();
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());

            Configurations.Configuration.Create()
                .UseAutofac(builder)
                .RegisterCommonComponent()
                .UseLog4Net()
                .UseJson4Net()
                .UseDapper()
                .AutofacBuild();

            /*  var webAssembly = Assembly.GetExecutingAssembly();
              var container = (ObjectContainer.Current as AutofacObjectContainer).Container;
              var builder = new ContainerBuilder();
              builder.RegisterControllers(webAssembly);
              builder.Update(container);
              DependencyResolver.SetResolver(new AutofacDependencyResolver(container));*/
            

            DapperRepository_Tests dapperRepository_Tests = new DapperRepository_Tests();
            dapperRepository_Tests.Add();
        }
    }
}
