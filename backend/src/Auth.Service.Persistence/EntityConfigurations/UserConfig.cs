using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Service.Persistence.EntityConfigurations;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.WithTrackerDbEntity();

        builder.Property(u => u.Name)            
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.Property(u => u.Active)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(u => u.CreatedAt)
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();

        builder.Property(u => u.UpdatedAt)
            .IsRequired(false);

        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder
            .HasMany<UserAuthProvider>()
            .WithOne(uap => uap.User)
            .HasForeignKey(uap => uap.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.Employees)
           .WithOne(e => e.User)
           .HasForeignKey(e => e.UserId)
           .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.UserTokens)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.CustomerAddresses) 
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
