using Auth.Service.Application.Addresses.Commands.Create;
using Auth.Service.Application.Addresses.DTOs;
using Auth.Service.Application.Addresses.Models;
using Auth.Service.Domain.Entities;
using Auth.Service.Domain.Utils;
using Mapster;

namespace Auth.Service.Application.Integrations;

public class MappingConfig : TypeAdapterConfig
{
    public MappingConfig()
    {
        TypeAdapterConfig<CreateAddressCommand, CustomerAddress>
            .NewConfig()
            .Ignore(dest => dest.DeliveryAddress);

        TypeAdapterConfig<DeliveryAddress, Address>
            .NewConfig()
            .Ignore(dest => dest.CustomerAddress);

        TypeAdapterConfig<ViaCepResponse, SearchAddressViewModel>
            .NewConfig()
            .Map(dest => dest.Street, src => src.Logradouro)
            .Map(dest => dest.Neighborhood, src => src.Bairro)
            .Map(dest => dest.City, src => src.Localidade)
            .Map(dest => dest.State, src => src.Uf)
            .Map(dest => dest.Complement, src => src.Complemento)
            .Map(dest => dest.ZipCode, src => src.Cep.OnlyNumbers());
    }

}
