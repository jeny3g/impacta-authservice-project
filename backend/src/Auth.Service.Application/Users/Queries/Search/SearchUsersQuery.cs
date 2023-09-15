using Auth.Service.Application.Users.Models;

namespace Auth.Service.Application.Users.Queries.Search;

public class SearchUsers
{
    public List<Guid> Ids { get; set; }

    public string Name { get; set; }
    public string Email { get; set; }
    public bool? Active { get; set; }
}

public class SearchUsersQuery : IRequest<PageResponse<UserSearchViewModel>>
{
    public List<Guid> Ids { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public bool? Active { get; set; }

    public PageRequest PageRequest { get; set; }

    public Ordination Ordination { get; set; }
}