using DotCommon.DapperTest.Repository;
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
    public class DapperRepository_Tests2
    {
        public void Add()
        {
            Product product = new Product { Name="adsfa", TenantId=1 };
            int n= new ProductRepository().InsertAndGetId(product);
        }


    }
}
