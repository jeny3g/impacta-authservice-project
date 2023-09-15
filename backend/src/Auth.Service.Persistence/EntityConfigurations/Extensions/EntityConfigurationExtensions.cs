using Auth.Service.Domain.Entities.General;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Service.Persistence.EntityConfigurations.Extensions;

public static class IntegrationEntityConfig
{
    public static void WithDbEntity<T>(this EntityTypeBuilder<T> builder)
        where T : DbEntity
    {
        builder.ToTable(typeof(T).Name);
    }

    public static void WithSimpleDbEntity<T>(this EntityTypeBuilder<T> builder)
        where T : SimpleDbEntity
    {
        builder.WithDbEntity();

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever();
    }

    public static void WithComplexDbEntity<T>(this EntityTypeBuilder<T> builder)
        where T : ComplexDbEntity
    {
        builder.WithDbEntity();

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();
    }

    public static void WithTrackerDbEntity<T>(this EntityTypeBuilder<T> builder)
        where T : TrackerDbEntity
    {
        builder.WithComplexDbEntity();

        builder.Property(u => u.CreatedAt)
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();

        builder.Property(u => u.UpdatedAt)
            .IsRequired(false);
    }
}
