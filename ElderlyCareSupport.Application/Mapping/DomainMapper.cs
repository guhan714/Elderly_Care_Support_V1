using ElderlyCareSupport.Application.DTOs;
using ElderlyCareSupport.Domain.Models;

namespace ElderlyCareSupport.Application.Mapping;

public static class MapToDomain
{
    public static List<FeeConfigurationDto> ToFeeConfigurationDto(List<FeeConfiguration> feeConfiguration)
    {
        return feeConfiguration.Select(feeConfigurationDto => new FeeConfigurationDto()
        {
            FeeId = feeConfigurationDto.FeeId, 
            FeeName = feeConfigurationDto.FeeName,
            FeeAmount = feeConfigurationDto.FeeAmount,
            Description = feeConfigurationDto.Description
        }).ToList();
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

    public static VolunteerUserDto ToVolunteerUserDto(VolunteerAccount volunteerAccount)
    {
        return new VolunteerUserDto
        {
            FirstName = volunteerAccount.FirstName,
            LastName = volunteerAccount.LastName,
            Email = volunteerAccount.Email,
            PhoneNumber = volunteerAccount.PhoneNumber,
            Address = volunteerAccount.Address,
            City = volunteerAccount.City,
            Country = volunteerAccount.Country,
            Region = volunteerAccount.Region,
            PostalCode = volunteerAccount.PostalCode,
            Gender = volunteerAccount.Gender,
            UserType = volunteerAccount.UserType
        };
    }
}