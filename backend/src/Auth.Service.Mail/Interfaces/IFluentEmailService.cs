namespace Auth.Service.Mail.Interfaces;

public interface IFluentEmailService
{
    Task SendConfirmationEmailAsync(string recipientEmail, string username, string confirmationUrl);

    Task SendPasswordResetEmailAsync(string recipientEmail, string username, string resetPasswordUrl);
}
