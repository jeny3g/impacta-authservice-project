using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Service.Persistence.EntityConfigurations;

public class CustomerAddressConfig : IEntityTypeConfiguration<CustomerAddress>
{
    public void Configure(EntityTypeBuilder<CustomerAddress> builder)
    {
        builder.WithTrackerDbEntity();

        builder.Property(c => c.ReceiverName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Phone)
            .HasMaxLength(15);

        builder.Property(u => u.Active)
            .HasDefaultValue(true)
            .IsRequired();

        builder.HasOne(c => c.User)
            .WithMany(u => u.CustomerAddresses)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);


        builder
            .HasOne(c => c.DeliveryAddress)
            .WithOne(a => a.CustomerAddress)
            .HasForeignKey<Address>(a => a.CustomerAddressId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(c => c.IsPrimaryAddress)
            .HasDefaultValue(false)
            .IsRequired();
    }
}
