using ElderlyCareSupport.Server.ViewModels;

namespace ElderlyCareSupport.Server.Interfaces
{
    public interface IRegistrationRepository
    {
        Task<bool> RegisterUser(RegistrationViewModel registrationViewModel);
    }
}
