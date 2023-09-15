using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Service.Persistence.EntityConfigurations;

public class RoleConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.WithTrackerDbEntity();

        builder.Property(r => r.Name).IsRequired().HasMaxLength(100);

        builder.Property(r => r.Description).HasMaxLength(500);


        builder.HasMany(r => r.Employees)
            .WithOne(e => e.Role)
            .HasForeignKey(e => e.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
