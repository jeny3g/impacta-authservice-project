using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Service.Persistence.EntityConfigurations;

public class AuthProviderConfig : IEntityTypeConfiguration<AuthProvider>
{
    public void Configure(EntityTypeBuilder<AuthProvider> builder)
    {
        builder.HasKey(ap => ap.Id);

        builder.HasMany<UserAuthProvider>()
            .WithOne(uap => uap.AuthProvider)
            .HasForeignKey(uap => uap.AuthProviderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}