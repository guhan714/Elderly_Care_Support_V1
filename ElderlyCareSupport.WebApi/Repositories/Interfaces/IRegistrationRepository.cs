using ElderlyCareSupport.Server.ViewModels;

namespace ElderlyCareSupport.Server.Repositories.Interfaces
{
    public interface  IRegistrationRepository
    {
        Task<bool> RegisterUser(RegistrationRequest registrationRequest);
        Task<bool> CheckExistingUser(string email);
    }
}
