namespace Auth.Service.Mail.Models;

public class ResetPasswordEmailModel
{
    public string Username { get; set; }

    public string ResetPasswordLink { get; set; }
}

