using Auth.Service.Application.Users.Models;

namespace Auth.Service.Application.Users.Queries.Get;

public class GetUserQuery : IRequest<UserViewModel>
{
    public Guid Id { get; set; }
}