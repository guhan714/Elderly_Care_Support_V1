using ElderlyCareSupport.Application.Contracts;
using ElderlyCareSupport.Application.DTOs;
using ElderlyCareSupport.Domain.Models;

namespace ElderlyCareSupport.Application.Helpers;

public static class DomainToDtoMapper
{
    public static IEnumerable<FeeConfigurationDto> ToFeeConfigurationDto(IEnumerable<FeeConfiguration> feeConfiguration)
    {
        return feeConfiguration.Select(fee => new FeeConfigurationDto
        {
            FeeId = fee.FeeId,
            FeeName = fee.FeeName,
            FeeAmount = fee.FeeAmount,
            Description = fee.Description
        });
    }

    public static ElderUserDto ToElderUserDto(ElderCareAccount elderCareAccount)
    {
        return new ElderUserDto
        {
            FirstName = elderCareAccount.FirstName,
            LastName = elderCareAccount.LastName,
            Email = elderCareAccount.Email,
            PhoneNumber = elderCareAccount.PhoneNumber,
            Address = elderCareAccount.Address,
            City = elderCareAccount.City,
            Country = elderCareAccount.Country,
            Region = elderCareAccount.Region,
            PostalCode = elderCareAccount.PostalCode,
            Gender = elderCareAccount.Gender,
            UserType = elderCareAccount.UserType
        };
    }
}



public static class DtoToDomainMapper
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