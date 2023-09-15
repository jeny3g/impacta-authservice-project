using System.Security.Cryptography;
using System.Text;

namespace Auth.Service.Persistence;

public class AuthInitializer
{
    public static void Initialize(AuthContext context)
    {
        var instance = new AuthInitializer();
        instance.Seed(context);
    }

    private void Seed(AuthContext context)
    {
        SeedAuthProviders(context);
        SeedRoles(context);
        SeedUsers(context);
        SeedEmployees(context);
        SeedUserAuthProviders(context);
    }

    private void SeedAuthProviders(AuthContext context)
    {
        if (!context.AuthProviders.Any())
        {
            var authProviders = new List<AuthProvider>
            {
                new AuthProvider { Id = Guid.NewGuid(), Name = "Google" },
                new AuthProvider { Id = Guid.NewGuid(), Name = "Facebook" },
            };

            context.AuthProviders.AddRange(authProviders);

            context.SaveChanges();
        }
    }

    private void SeedRoles(AuthContext context)
    {
        if (!context.Roles.Any())
        {
            var roles = new List<Role>
            {
                new Role { Id = Guid.NewGuid(), Name = "Admin", Description = "Administrator with full rights" },
                new Role { Id = Guid.NewGuid(), Name = "User", Description = "Standard user" },
            };

            context.Roles.AddRange(roles);

            context.SaveChanges();
        }
    }

    private void SeedUsers(AuthContext context)
    {
        if (!context.Users.Any())
        {
            // For the purpose of seeding, we're using a simple hash function.
            // In a real application, you should use a secure method for generating and storing password hashes.
            var passwordHash = GetPasswordHash("password");

            var users = new List<User>
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin",
                    Email = "admin@test.com",
                    PasswordHash = passwordHash,
                    Active = true
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Name = "User",
                    Email = "user@test.com",
                    PasswordHash = passwordHash,
                    Active = true
                },
                // Add more users as needed
            };

            context.Users.AddRange(users);

            context.SaveChanges();
        }
    }

    private void SeedEmployees(AuthContext context)
    {
        if (!context.Employees.Any())
        {
            var adminUser = context.Users.FirstOrDefault(u => u.Email == "admin@test.com");
            var normalUser = context.Users.FirstOrDefault(u => u.Email == "user@test.com");
            var adminRole = context.Roles.FirstOrDefault(r => r.Name == "Admin");
            var userRole = context.Roles.FirstOrDefault(r => r.Name == "User");

            if (adminUser != null && normalUser != null && adminRole != null && userRole != null)
            {
                var employees = new List<Employee>
                {
                    new Employee
                    {
                        Id = Guid.NewGuid(),
                        UserId = adminUser.Id,
                        RoleId = adminRole.Id,
                        Name = "Admin Employee",
                        Phone = "+1-234-567-8903",
                        Active = true
                    },
                    new Employee
                    {
                        Id = Guid.NewGuid(),
                        UserId = normalUser.Id,
                        RoleId = userRole.Id,
                        Name = "User Employee",
                        Phone = "+1-234-567-8904",
                        Active = true
                    },
                    // Add more employees as needed
                };

                context.Employees.AddRange(employees);
            }

            context.SaveChanges();
        }
    }

    private void SeedUserAuthProviders(AuthContext context)
    {
        if (!context.UserAuthProviders.Any())
        {
            var adminUser = context.Users.FirstOrDefault(u => u.Email == "admin@test.com");
            var normalUser = context.Users.FirstOrDefault(u => u.Email == "user@test.com");
            var googleAuthProvider = context.AuthProviders.FirstOrDefault(ap => ap.Name == "Google");
            var facebookAuthProvider = context.AuthProviders.FirstOrDefault(ap => ap.Name == "Facebook");

            if (adminUser != null && normalUser != null && googleAuthProvider != null && facebookAuthProvider != null)
            {
                var userAuthProviders = new List<UserAuthProvider>
                {
                    new UserAuthProvider
                    {
                        ExternalUserId = "admin-google",
                        UserId = adminUser.Id,
                        AuthProviderId = googleAuthProvider.Id
                    },
                    new UserAuthProvider
                    {
                        ExternalUserId = "admin-facebook",
                        UserId = adminUser.Id,
                        AuthProviderId = facebookAuthProvider.Id
                    },
                    new UserAuthProvider
                    {
                        ExternalUserId = "user-google",
                        UserId = normalUser.Id,
                        AuthProviderId = googleAuthProvider.Id
                    },
                    new UserAuthProvider
                    {
                        ExternalUserId = "user-facebook",
                        UserId = normalUser.Id,
                        AuthProviderId = facebookAuthProvider.Id
                    },
                    // Add more UserAuthProvider as needed
                };

                context.UserAuthProviders.AddRange(userAuthProviders);
            }

            context.SaveChanges();
        }
    }

    private string GetPasswordHash(string password)
    {
        // This is a simple hashing function for illustration purposes only.
        // You should use a secure method for hashing passwords in a real application.
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
}
