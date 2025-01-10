using ElderlyCareSupport.Application.Contracts.Login;
using ElderlyCareSupport.Domain.Models;

namespace ElderlyCareSupport.Application.IRepository
{
    public interface ILoginRepository
    {
        Task<bool> AuthenticateLogin(LoginRequest loginCredentials);
    }
}
