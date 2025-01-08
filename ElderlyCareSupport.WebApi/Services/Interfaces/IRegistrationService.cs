using ElderlyCareSupport.Server.ViewModels;

namespace ElderlyCareSupport.Server.Services.Interfaces
{
    public interface IRegistrationService
    {
        Task<bool> RegisterUserAsync(RegistrationRequest registrationRequest);
        Task<bool> CheckUserExistingAlready(string email);
    }
}
