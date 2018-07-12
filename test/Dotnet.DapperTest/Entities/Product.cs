using Dotnet.Data;
using Dapper;
using DapperExtensions;
using DapperExtensions.Mapper;

namespace Dotnet.Dapper.Tests.Entities
{
    public class Product : IEntity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? TenantId { get; set; }
    }


    public sealed class ProductMap : ClassMapper<Product>
    {
        public ProductMap()
        {
            Table("Product");
            Map(x => x.Id).Key(KeyType.Identity);
            AutoMap();
        }
    }


}
