using Auth.Service.Domain.Entities.General;

namespace Auth.Service.Persistence;

public class AuthContext : DbContext, IAuthContext
{
    public AuthContext(DbContextOptions<AuthContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserToken> UserTokens { get; set; }
    public virtual DbSet<CustomerAddress> CustomerAddresses  { get; set; }
    public virtual DbSet<AuthProvider> AuthProviders { get; set; }
    public virtual DbSet<UserAuthProvider> UserAuthProviders { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthInitializer).Assembly);

        // Lower Table Names
        modelBuilder.Model.GetEntityTypes()
            .ToList()
            .ForEach(t => t.SetTableName(t.GetTableName().ToLower()));

        // Lower column names
        modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetProperties())
            .ToList()
            .ForEach(p => p.SetColumnName(p.GetColumnName().ToLower()));
    }

    public override int SaveChanges()
    {
        SetDateOnEntities();

        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetDateOnEntities();

        return base.SaveChangesAsync();
    }

    private void SetDateOnEntities()
    {
        var entries = ChangeTracker
           .Entries()
           .Where(e => e.Entity is TrackerDbEntity && (
                   e.State == EntityState.Added
                   || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Modified)
            {
                ((TrackerDbEntity)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
            }

            if (entityEntry.State == EntityState.Added)
            {
                ((TrackerDbEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
            }
        }
    }
}
