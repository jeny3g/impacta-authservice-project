using System.Diagnostics.CodeAnalysis;

[assembly: ExcludeFromCodeCoverage]
namespace Auth.Service.Persistence;

public interface IAuthContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    DbSet<User> Users { get; set; }
    DbSet<UserToken> UserTokens { get; set; }
    DbSet<CustomerAddress> CustomerAddresses  { get; set; }
    DbSet<AuthProvider> AuthProviders { get; set; }
    DbSet<UserAuthProvider> UserAuthProviders { get; set; }
    DbSet<Employee> Employees { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<Address> Addresses { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
