using ElderlyCareSupport.Application.Contracts.Requests;
using ElderlyCareSupport.Application.Contracts.Response;

namespace ElderlyCareSupport.Application.IService
{
    public interface ILoginService
    {
        Task<Tuple<LoginResponse?, bool>> AuthenticateLogin(LoginRequest loginRequest);
    }
}
