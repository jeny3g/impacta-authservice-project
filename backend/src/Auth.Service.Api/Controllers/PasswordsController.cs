using Auth.Service.Application.Passwords.Commands.Forgot;
using Auth.Service.Application.Passwords.Commands.Reset;

namespace Auth.Service.Api.Controllers;

public class PasswordsController : BaseController
{
    public PasswordsController(IMediator mediator) : base(mediator) { }

    [HttpPost("forgot")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreateSuccess))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<CreateSuccess>> ForgotPassword([FromBody] ForgotPassword model)
    {
        var command = model.Adapt<ForgotPasswordCommand>();

        return Ok(await _mediator.Send(command));
    }

    [HttpPost("reset")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreateSuccess))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<CreateSuccess>> Reset([FromBody] ResetPassword model)
    {
        var command = model.Adapt<ResetPasswordCommand>();

        return Ok(await _mediator.Send(command));
    }
}

