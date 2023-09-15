namespace Auth.Service.Application.Users.Models;

public class UserViewModel : ETrackerViewModel
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string Active { get; set; }
}