using Auth.Service.Application.Settings;
using Auth.Service.Application.Users.Models;
using Microsoft.AspNetCore.Http;
using Auth.Service.Application.Base;

namespace Auth.Service.Application.Users.Queries.Search;

public class SearchUsersQueryHandler : AuthenticatedHandler, IRequestHandler<SearchUsersQuery, PageResponse<UserSearchViewModel>>
{
    private static readonly int MAX_ITEMS_PER_PAGE = 50;

    private static readonly Dictionary<string, Func<bool, IQueryable<User>, IQueryable<User>>> _users = new(StringComparer.InvariantCultureIgnoreCase)
    {
        { nameof(User.Name), (asc, query) => asc ? query.OrderBy(e => e.Name) : query.OrderByDescending(e => e.Name) },
        { nameof(User.Email), (asc, query) => asc ? query.OrderBy(e => e.Email) : query.OrderByDescending(e => e.Email) },
        { nameof(User.Active), (asc, query) => asc ? query.OrderBy(e => e.Active) : query.OrderByDescending(e => e.Active) },
        { nameof(User.CreatedAt), (asc, query) => asc ? query.OrderBy(e => e.CreatedAt) : query.OrderByDescending(e => e.CreatedAt) },
        { nameof(User.UpdatedAt), (asc, query) => asc ? query.OrderBy(e => e.UpdatedAt) : query.OrderByDescending(e => e.UpdatedAt) },
    };

    public static SearchOptions SearchOptions => new(MAX_ITEMS_PER_PAGE, _users.Keys.ToList());

    private readonly IAuthContext _context;

    public SearchUsersQueryHandler(IAuthContext context, IHttpContextAccessor httpContextAccessor)
        : base(httpContextAccessor)
    {
        _context = context;
    }

    public async Task<PageResponse<UserSearchViewModel>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
    {
        request ??= new SearchUsersQuery();

        var query = _context.Users.AsQueryable();

        if(request.Ids.AnySafe())
            query = query.Where(e => request.Ids.Contains(e.Id));

        if (!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(e => e.Name.Contains(request.Name));

        if (!string.IsNullOrWhiteSpace(request.Email))
            query = query.Where(e => e.Email.Contains(request.Email));

        if (request.Active.HasValue)
            query = query.Where(e => e.Active.Equals((bool)request.Active));

        var ordination = request.Ordination;

        var user = _users.GetValueOrDefault(ordination.OrderBy ?? string.Empty, (asc, query) => query.OrderBy(e => e.CreatedAt));

        query = user(ordination.Asc, query);

        var pageRequest = request.PageRequest ?? PageRequest.First();

        var projection = query.ProjectToType<UserSearchViewModel>();

        return await Pagination.Paginate(projection, pageRequest.WithMaxLimit(MAX_ITEMS_PER_PAGE));
    }
}