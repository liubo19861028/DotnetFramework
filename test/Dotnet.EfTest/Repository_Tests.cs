using Dotnet.Data;
using Dotnet.Data.Providers;
using Dotnet.Dependency;
using Dotnet.EfTest.Tests.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.EfTest
{
    public class Repository_Tests
    {
        //private readonly RepositoryBase<Product> _productRepository;
        private readonly RepositoryBase<Product, int> _productRepository;

        public Repository_Tests()
        {
            // _productRepository = IocManager.GetContainer().Resolve<RepositoryBase<Product>>();
            _productRepository = IocManager.GetContainer().Resolve<RepositoryBase<Product, int>>();
        }

        public void Add()
        {
            Product product = new Product { Name="adsfa", TenantId=1 };
            int n= _productRepository.InsertAndGetId(product);
        }


    }
}
