using ElderlyCareSupport.Application.Contracts;

namespace ElderlyCareSupport.Application.IService
{
    public interface IRegistrationService
    {
        Task<bool> RegisterUserAsync(RegistrationRequest registrationRequest);
        Task<bool> CheckUserExistingAlready(string email);
    }
}
