namespace ElderlyCareSupport.Server.Services.Interfaces
{
    public interface IForgotPaswordService
    {
        Task<string> GetForgotPassword(string userName);
    }
}
