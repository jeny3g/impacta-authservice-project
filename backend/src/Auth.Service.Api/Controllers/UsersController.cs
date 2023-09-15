using Auth.Service.Application.Users.Commands.Create;
using Auth.Service.Application.Users.Commands.Verify;
using Auth.Service.Application.Users.Models;
using Auth.Service.Application.Users.Queries.Get;
using Auth.Service.Application.Users.Queries.Search;
using Microsoft.AspNetCore.Authorization;

namespace Auth.Service.Api.Controllers;

public class UsersController : BaseController
{
	public UsersController(IMediator mediator) : base(mediator){ }

    [HttpGet("searchOptions")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SearchOptions))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ProblemDetails))]
    public ActionResult<SearchOptions> GetOrderOptions() => Ok(SearchUsersQueryHandler.SearchOptions);

    [Authorize]
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PageResponse<UserSearchViewModel>))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<PageResponse<UserSearchViewModel>>> Search(
        [FromQuery] SearchUsers model,
        [FromQuery] PageRequestDto request,
        [FromQuery] OrdinationDto ordination)
    {
        var query = model.Adapt<SearchUsersQuery>();
        query.PageRequest = PageRequest.Of(request.Number, request.Limit);
        query.Ordination = ordination.Adapt<Ordination>();

        return Ok(await _mediator.Send(query));
    }

    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(UserViewModel))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<UserViewModel>> Get(Guid id)
    {
        return Ok(await _mediator.Send(new GetUserQuery()
        {
            Id = id
        }));
    }

    [HttpPost("create")]
	[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreateSuccess))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<CreateSuccess>> Create([FromBody] CreateUser model)
    {
        var command = model.Adapt<CreateUserCommand>();

        return Ok(await _mediator.Send(command));
    }

    [HttpPut("verify")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreateSuccess))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<CreateSuccess>> Create([FromBody] VerifyUser model)
    {
        var command = model.Adapt<VerifyUserCommand>();

        return Ok(await _mediator.Send(command));
    }
}

