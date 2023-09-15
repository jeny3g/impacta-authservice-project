using Auth.Service.Application.Addresses.Models;
using Auth.Service.Application.ExternalServices;

namespace Auth.Service.Application.Addresses.Queries.GetAddress;

public class GetAddressQuery : IRequest<SearchAddressViewModel>
{
    public string ZipCode { get; set; }
}

public class GetAddressQueryHandler : IRequestHandler<GetAddressQuery, SearchAddressViewModel>
{
    private readonly IViaCEPClient _viaCepClient;

    public GetAddressQueryHandler(IViaCEPClient viaCepClient)
    {
        _viaCepClient = viaCepClient;
    }

    public async Task<SearchAddressViewModel> Handle(GetAddressQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return await _viaCepClient.GetAddressByCEP(request.ZipCode);
        }
        catch (Exception e)
        {
            throw new Exception($"Error on get address by CEP: {request.ZipCode}", e);
        }
    }
}