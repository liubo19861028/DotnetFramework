using Autofac;
using Dotnet.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            Configurations.Configuration.Create()
               .UseAutofac(builder)
               .RegisterCommonComponent()
               .UseLog4Net()
               .AutofacBuild();

            using (var service = new DotnetServices()) service.Start(args);
        }
    }
}
