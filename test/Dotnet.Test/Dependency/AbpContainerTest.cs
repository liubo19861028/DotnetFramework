using Dotnet.Configurations;
using Dotnet.Test.Dependency.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Dotnet.Test.Dependency
{
    public class AbpContainerTest
    {
        //[Fact]
        //public void AbpContainer_Test()
        //{
        //    Configurations.Configuration.Create()
        //        .UseAbpContainer(Abp.Dependency.IocManager.Instance.IocContainer);

        //    var container = Dotnet.Dependency.IocManager.GetContainer();
        //    container.Register<InterfaceContainerTest, ContainerTestImpl1>(Dotnet.Dependency.DependencyLifeStyle.Transient, "impl1");
        //    container.Register<InterfaceContainerTest, ContainerTestImpl2>(Dotnet.Dependency.DependencyLifeStyle.Transient, "impl2");

        //    var impl1 = container.ResolveNamed<InterfaceContainerTest>("impl1");
        //    var impl2 = container.ResolveNamed<InterfaceContainerTest>("impl2");

        //    Assert.Equal("ContainerTestImpl1", impl1.GetName());
        //    Assert.Equal("ContainerTestImpl2", impl2.GetName());

        //}
    }
}
