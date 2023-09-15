using Auth.Service.Mail.Interfaces;
using Auth.Service.Mail.Models;
using FluentEmail.Core.Interfaces;
using FluentEmail.Core;
using FluentEmail.Razor;

namespace Auth.Service.Mail;

public class FluentEmailService : IFluentEmailService
{
    private const string DEFAULT_EMAIL_FROM = "mirandajean2009@outlook.com";

    private readonly ISender _emailSender;

    public FluentEmailService(ISender emailSender)
    {
        _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        Email.DefaultSender = _emailSender;
        Email.DefaultRenderer = new RazorRenderer();
    }

    public async Task SendConfirmationEmailAsync(string recipientEmail, string username, string confirmationUrl)
    {
        var emailModel = new ConfirmationEmailModel
        {
            Username = username,
            ConfirmationLink = confirmationUrl
        };
        var templatePath = GetTemplatePath("ConfirmationEmailTemplate.cshtml");

        var email = Email
            .From(DEFAULT_EMAIL_FROM)
            .To(recipientEmail)
            .Subject("Confirme seu email")
            .Tag("confirmationTag")
            .UsingTemplateFromFile(templatePath, emailModel);

        try
        {
            await _emailSender.SendAsync(email);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new Exception("Error sending email");
        }
    }

    public async Task SendPasswordResetEmailAsync(string recipientEmail, string username, string resetPasswordUrl)
    {
        var emailModel = new ResetPasswordEmailModel
        {
            Username = username,
            ResetPasswordLink = resetPasswordUrl
        };
        var templatePath = GetTemplatePath("ResetPasswordEmailTemplate.cshtml");

        var email = Email
            .From(DEFAULT_EMAIL_FROM)
            .To(recipientEmail)
            .Subject("Esqueceu a senha?")
            .Tag("resetPasswordTag")
            .UsingTemplateFromFile(templatePath, emailModel);

        try
        {
            await _emailSender.SendAsync(email);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new Exception("Error sending email");
        }
    }

    private string GetTemplatePath(string templateName)
    {
        var currentAssembly = System.Reflection.Assembly.GetExecutingAssembly();
        var assemblyLocation = currentAssembly.Location;
        var directoryOfAssembly = Path.GetDirectoryName(assemblyLocation);
        return Path.Combine(directoryOfAssembly, "EmailTemplates", templateName);
    }
}
