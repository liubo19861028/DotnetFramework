using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dotnet.Data.Providers;
using Dotnet.Utility;
using Dotnet.Dependency;
using Autofac;
using Dotnet.Configurations;
using System.Reflection;
using Dotnet.Data;

namespace Dotnet.AdoTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DbContext>().As<IDbContext>();

            Configurations.Configuration.Create()
                .UseAutofac(builder)
                .RegisterCommonComponent()
                .UseLog4Net()
                .UseAdo()
                .AutofacBuild();


            Repository_Tests repository_Tests = new Repository_Tests();
            repository_Tests.GetModel();
            repository_Tests.Add();
        }
    }
}
