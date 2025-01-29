using ElderlyCareSupport.Application.Contracts.Requests;
using ElderlyCareSupport.Domain.Models;

namespace ElderlyCareSupport.Application.Mapping;

public static class MapToDto
{
    public static ElderCareAccount ToElderCareAccount(RegistrationRequest registrationRequest)
    {
        return new ElderCareAccount()
        {
            FirstName = registrationRequest.FirstName,
            LastName = registrationRequest.LastName,
            Email = registrationRequest.Email,
            Password = registrationRequest.Password,
            PhoneNumber = registrationRequest.PhoneNumber,
            Address = registrationRequest.Address,
            City = registrationRequest.City,
            Country = registrationRequest.Country,
            Region = registrationRequest.Region,
            PostalCode = registrationRequest.PostalCode,
            Gender = registrationRequest.Gender
        };
    }
}