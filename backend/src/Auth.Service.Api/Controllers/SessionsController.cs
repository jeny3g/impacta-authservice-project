using Auth.Service.Application.Sessions.Commands.Authenticate;

namespace Auth.Service.Api.Controllers;

public class SessionsController : BaseController
{
    public SessionsController(IMediator mediator) : base(mediator) { }

    [HttpPost("authenticate")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreateSuccess))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<CreateSuccess>> Authenticate([FromBody] AuthenticateSession model)
    {
        var command = model.Adapt<AuthenticateSessionCommand>();

        return Ok(await _mediator.Send(command));
    }
}

