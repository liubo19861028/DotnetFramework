using Dotnet.Dapper;
using Dotnet.Dapper.Tests.Entities;
using Dotnet.DapperTest;
using Dotnet.Data.Providers;
using Dotnet.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotCommon.DapperTest.Repository
{
    public class ProductRepository: DapperRepositoryBase<Product,int>
    {
        public ProductRepository()
        {
            var container = IocManager.GetContainer();
            base._activeTransactionProvider = container.Resolve<IActiveTransactionProvider>();
            //base._activeTransactionProvider= new DapperActiveTransactionProvider(new DapperDbContext());
        }
    }
}
