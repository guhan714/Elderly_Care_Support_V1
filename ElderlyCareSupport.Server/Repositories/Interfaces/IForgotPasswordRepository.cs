namespace ElderlyCareSupport.Server.Repositories.Interfaces
{
    public interface IForgotPasswordRepository
    {
        Task<string?> GetPasswordAsync(string userName);
    }
}
