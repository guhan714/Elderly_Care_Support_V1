using ElderlyCareSupport.Application.Contracts.Requests;

namespace ElderlyCareSupport.Application.IRepository
{
    public interface  IRegistrationRepository
    {
        Task<bool> RegisterUser(RegistrationRequest registrationRequest);
        Task<bool> CheckExistingUser(string email);
    }
}
