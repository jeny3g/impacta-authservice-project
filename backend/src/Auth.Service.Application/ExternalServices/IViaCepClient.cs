using Auth.Service.Application.Addresses.Models;

namespace Auth.Service.Application.ExternalServices;

public interface IViaCEPClient
{
    Task<SearchAddressViewModel> GetAddressByCEP(string zipcode);
}