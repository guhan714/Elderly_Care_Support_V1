using AutoMapper;
using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Models;
using ElderlyCareSupport.Server.ViewModels;

namespace ElderlyCareSupport.Server.Helpers
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistrationRequest, ElderCareAccount>();
            CreateMap<ElderCareAccount, ElderUserDto>();
            CreateMap<FeeConfiguration, FeeConfigurationDto>();
        }
    }

}
