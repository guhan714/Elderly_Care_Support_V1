using ElderlyCareSupport.Server.ViewModels;

namespace ElderlyCareSupport.Server.Services.Interfaces
{
    public interface IRegistrationService
    {
        Task<bool> RegisterUser(RegistrationViewModel registrationViewModel);
    }
}
