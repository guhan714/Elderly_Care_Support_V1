using ElderlyCareSupport.Application.Contracts.Requests;

namespace ElderlyCareSupport.Application.IRepository
{
    public interface ILoginRepository
    {
        Task<bool> AuthenticateLogin(LoginRequest loginCredentials);
    }
}
