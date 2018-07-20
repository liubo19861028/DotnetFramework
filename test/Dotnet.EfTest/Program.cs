using Autofac;
using Dotnet.Configurations;
using Dotnet.Data.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.EfTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<EfDbContext>().As<IDbContext>();
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());

            Configurations.Configuration.Create()
                .UseAutofac(builder)
                .RegisterCommonComponent()
                .UseLog4Net()
                .UseEf()
                .AutofacBuild();

            /*  var webAssembly = Assembly.GetExecutingAssembly();
              var container = (ObjectContainer.Current as AutofacObjectContainer).Container;
              var builder = new ContainerBuilder();
              builder.RegisterControllers(webAssembly);
              builder.Update(container);
              DependencyResolver.SetResolver(new AutofacDependencyResolver(container));*/


            Repository_Tests repository_Tests = new Repository_Tests();
            repository_Tests.Add();
        }
    }
}
