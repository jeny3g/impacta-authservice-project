namespace Auth.Service.Application.Services.Interfaces;

public interface IPasswordHashService
{
    string HashPassword(string password);
    bool VerifyPassword(string hash, string password);
}
