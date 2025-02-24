namespace The_Engneering.Persistence.EntitiesConfigurations;

public class BillConfigurations : IEntityTypeConfiguration<Bill>
{
    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        builder.Property(product => product.CustomerName).HasMaxLength(50);
        builder.Property(product => product.CustomerPhone).HasMaxLength(11);
        builder.Property(bill => bill.Amount).IsRequired();
        builder.Property(bill => bill.TotalPrice).IsRequired();
    }
}

