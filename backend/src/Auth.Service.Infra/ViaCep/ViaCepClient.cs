using Auth.Service.Application.ExternalServices;
using Auth.Service.Application.Addresses.Models;
using Auth.Service.Domain.Settings;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using Auth.Service.Application.Addresses.DTOs;
using Mapster;

namespace Auth.Service.Infra.ViaCep;

public class ViaCepClient : BaseClient, IViaCEPClient
{
    protected string BASE_ADDRESS { get; set; }

    protected readonly ILogger<ViaCepClient> _logger;

    public ViaCepClient(HttpClient client, ILogger<ViaCepClient> logger) : base(client)
    {
        BASE_ADDRESS = EnvConstants.VIACEP_BASE_URL();

        _logger = logger;

        client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue() { NoCache = true };
    }

    public async Task<SearchAddressViewModel> GetAddressByCEP(string zipcode)
    {
        var uri = new Uri($"{BASE_ADDRESS}/ws/{zipcode}/json/");

        var response = await GetAsync<ViaCepResponse>(uri);

        if (response is null)
            return new SearchAddressViewModel();

        var address = response.Adapt<SearchAddressViewModel>();

        return address;
    }
}
