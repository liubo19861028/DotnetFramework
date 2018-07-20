using Dotnet.Data;

namespace Dotnet.EfTest.Tests.Entities
{
    public class Product : IEntity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? TenantId { get; set; }
    }


    public partial class ProductMap
        : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            // table
            ToTable("Product", "dbo");

            // keys
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .HasColumnName("Id")
                .IsRequired();
            Property(t => t.Name)
                .HasColumnName("Name")
                .HasMaxLength(255)
                .IsRequired();
           

            // Relationships
        }
    }

}
