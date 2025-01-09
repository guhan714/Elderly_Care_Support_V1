using ElderlyCareSupport.Application.Contracts.Login;

namespace ElderlyCareSupport.Application.IService
{
    public interface ILoginService
    {
        Task<Tuple<LoginResponse?, bool>> AuthenticateLogin(LoginRequest loginRequest);
    }
}
