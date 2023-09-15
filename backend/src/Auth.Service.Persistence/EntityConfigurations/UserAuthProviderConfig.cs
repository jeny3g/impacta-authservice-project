using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Service.Persistence.EntityConfigurations;

public class UserAuthProviderConfig : IEntityTypeConfiguration<UserAuthProvider>
{
    public void Configure(EntityTypeBuilder<UserAuthProvider> builder)
    {
        builder.HasKey(uap => new { uap.UserId, uap.AuthProviderId });

        builder
            .HasOne(uap => uap.User)
            .WithMany(u => u.UserAuthProviders)
            .HasForeignKey(uap => uap.UserId);

        builder
            .HasOne(uap => uap.AuthProvider)
            .WithMany(ap => ap.UserAuthProviders)
            .HasForeignKey(uap => uap.AuthProviderId);

        builder.Property(uap => uap.ExternalUserId).IsRequired();
    }
}
