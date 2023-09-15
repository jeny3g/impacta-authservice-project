using Auth.Service.Application.Addresses.Commands.Create;
using Auth.Service.Application.Addresses.Models;
using Auth.Service.Application.Addresses.Queries.GetAddress;
using Auth.Service.Application.Addresses.Queries.List;
using Microsoft.AspNetCore.Authorization;

namespace Auth.Service.Api.Controllers;

[Authorize]
public class AddressesController : BaseController
{
    public AddressesController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(List<AddressViewModel>))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<List<AddressViewModel>>> ListAll()
    {
        return Ok(await _mediator.Send(new ListAddressQuery()));
    }

    [HttpGet("zipcode/{zipcode}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(SearchAddressViewModel))]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<SearchAddressViewModel>> GetAddressByCEP(string zipcode)
    {
        return Ok(await _mediator.Send(new GetAddressQuery()
        {
            ZipCode = zipcode
        }));
    }

    [HttpPost("create")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreateSuccess))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(ProblemDetails))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult<CreateSuccess>> Create([FromBody] CreateAddress model)
    {
        var command = model.Adapt<CreateAddressCommand>();

        return Ok(await _mediator.Send(command));
    }
}