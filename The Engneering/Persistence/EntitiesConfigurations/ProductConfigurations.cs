
namespace The_Engneering.Persistence.EntitiesConfigurations;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(product => product.Name).HasMaxLength(100);
        builder.Property(product => product.ImageUrl).HasMaxLength(200);
        builder.Property(product => product.CountryOfOrigin).HasMaxLength(200);
    }
}
