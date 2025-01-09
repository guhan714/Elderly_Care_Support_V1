using ElderlyCareSupport.Application.Contracts.Login;

namespace ElderlyCareSupport.Application.IRepository
{
    public interface ILoginRepository
    {
        Task<bool> AuthenticateLogin(LoginRequest loginRequest);
    }
}
