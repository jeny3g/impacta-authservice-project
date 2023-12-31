﻿namespace Auth.Service.Application.Addresses.Models;

public class SearchAddressViewModel
{
    public string ZipCode { get; set; }

    public string City { get; set; }
    public string State { get; set; }

    public string Street { get; set; }
    public string Neighborhood { get; set; }

    public string? Complement { get; set; }
}