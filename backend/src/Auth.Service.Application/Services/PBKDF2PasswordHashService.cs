using System.Security.Cryptography;

namespace Auth.Service.Application.Services;

public class PBKDF2PasswordHashService : IPasswordHashService
{
    private const int SaltSize = 16; // 128 bit 
    private const int KeySize = 32; // 256 bit
    private const int Iterations = 10000;
    private const string HashFormat = "{0}.{1}.{2}";

    public string HashPassword(string password)
    {
        using (var algorithm = new Rfc2898DeriveBytes(password, SaltSize, Iterations, HashAlgorithmName.SHA256))
        {
            var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
            var salt = Convert.ToBase64String(algorithm.Salt);

            return FormatHashString(Iterations, salt, key);
        }
    }

    public bool VerifyPassword(string hash, string password)
    {
        var (iterations, salt, key) = ParseHashString(hash);

        using (var algorithm = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), iterations, HashAlgorithmName.SHA256))
        {
            var keyToCheck = algorithm.GetBytes(KeySize);

            return Convert.FromBase64String(key).SequenceEqual(keyToCheck);
        }
    }

    private string FormatHashString(int iterations, string salt, string key)
    {
        return string.Format(HashFormat, iterations, salt, key);
    }

    private (int, string, string) ParseHashString(string hash)
    {
        var parts = hash.Split('.', 3);

        if (parts.Length != 3)
            throw new FormatException($"Unexpected hash format. Should be formatted as `{HashFormat}`, but was `{hash}`");

        var iterations = Convert.ToInt32(parts[0]);
        var salt = parts[1];
        var key = parts[2];

        return (iterations, salt, key);
    }
}
