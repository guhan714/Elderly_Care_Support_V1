using ElderlyCareSupport.Server.ViewModels;

namespace ElderlyCareSupport.Server.Repositories.Interfaces
{
    public interface  IRegistrationRepository
    {
        Task<bool> RegisterUser(RegistrationViewModel registrationViewModel);
        Task<bool> CheckExistingUser(string email);
    }
}
