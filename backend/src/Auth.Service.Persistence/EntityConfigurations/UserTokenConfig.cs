using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Service.Persistence.EntityConfigurations;

public class UserTokenConfig : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.Property(t => t.Token)
            .IsRequired();

        builder.Property(t => t.ExpiresAt)
            .IsRequired();

        builder
            .HasOne(t => t.User)
            .WithMany(u => u.UserTokens)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
