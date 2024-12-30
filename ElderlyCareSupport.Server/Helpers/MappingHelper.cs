using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Models;

namespace ElderlyCareSupport.Server.Helpers;

public static class DomainToDtoMapper
{
    public static IEnumerable<FeeConfigurationDto> ToFeeConfigurationDto(IEnumerable<dynamic> feeConfiguration)
    {
        return feeConfiguration.Select(fee => new FeeConfigurationDto
        {
            FeeId = fee.FeeId,
            FeeName = fee.FeeName,
            FeeAmount = fee.FeeAmount
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
            Gender = elderCareAccount.Gender
        };
    }
}