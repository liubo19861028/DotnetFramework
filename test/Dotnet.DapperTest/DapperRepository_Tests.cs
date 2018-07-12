using Dotnet.Dapper.Dapper;
using Dotnet.Dapper.Tests.Entities;
using Dotnet.Data;
using Dotnet.Data.Providers;
using Dotnet.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.DapperTest
{
    public class DapperRepository_Tests
    {
        private readonly RepositoryBase<Product> _productDapperRepository;
        private readonly RepositoryBase<Product,int> _productRepository;

        public DapperRepository_Tests()
        {
            _productDapperRepository = IocManager.GetContainer().Resolve<RepositoryBase<Product>>();
            _productRepository = IocManager.GetContainer().Resolve<RepositoryBase<Product,int>>();
        }

        public void Add()
        {
            Product product = new Product { Name="adsfa", TenantId=1 };
            int n= _productRepository.InsertAndGetId(product);
        }


    }
}
