using ElderlyCareSupport.Server.ViewModels;

namespace ElderlyCareSupport.Server.Services.Interfaces
{
    public interface IRegistrationService
    {
        Task<bool> RegisterUserAsync(RegistrationViewModel registrationViewModel);
        Task<bool> checkUserExistingAlready(string email);
    }
}
