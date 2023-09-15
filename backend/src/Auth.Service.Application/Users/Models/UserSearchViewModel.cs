namespace Auth.Service.Application.Users.Models;

public class UserSearchViewModel : ETrackerViewModel
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string Active { get; set; }
}