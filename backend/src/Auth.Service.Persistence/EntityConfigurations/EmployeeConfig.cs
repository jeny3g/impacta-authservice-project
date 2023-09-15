using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Service.Persistence.EntityConfigurations;

public class EmployeeConfig : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.WithTrackerDbEntity();

        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);

        builder.Property(e => e.Phone).IsRequired().HasMaxLength(15);

        builder.HasOne(e => e.User)
            .WithMany(u => u.Employees)
            .HasForeignKey(e => e.UserId);

        builder.HasOne(e => e.Role)
            .WithMany(r => r.Employees)
            .HasForeignKey(e => e.RoleId);
    }
}
