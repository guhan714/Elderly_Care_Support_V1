using ElderlyCareSupport.Application.Contracts.Requests;

namespace ElderlyCareSupport.Application.IService
{
    public interface IRegistrationService
    {
        Task<bool> RegisterUserAsync(RegistrationRequest registrationRequest);
        Task<bool> CheckUserExistingAlready(string email);
    }
}
