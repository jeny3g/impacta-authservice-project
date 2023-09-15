namespace Auth.Service.Mail.Models;

public class ConfirmationEmailModel
{
    public string Username { get; set; }
    public string ConfirmationLink { get; set; }
}

