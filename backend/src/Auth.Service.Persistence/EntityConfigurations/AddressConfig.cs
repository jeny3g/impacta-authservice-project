using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Service.Persistence.EntityConfigurations;

public class AddressConfig : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.WithTrackerDbEntity();

        builder.Property(a => a.Street).IsRequired().HasMaxLength(100);
        builder.Property(a => a.City).IsRequired().HasMaxLength(50);
        builder.Property(a => a.State).HasMaxLength(50);
        builder.Property(a => a.ZipCode).HasMaxLength(20);
        builder.Property(a => a.Country).IsRequired().HasMaxLength(3);
        builder.Property(a => a.Neighborhood).HasMaxLength(50);
        builder.Property(a => a.StreetNumber).HasMaxLength(15);
        builder.Property(a => a.Complement).HasMaxLength(100);
    }
}
