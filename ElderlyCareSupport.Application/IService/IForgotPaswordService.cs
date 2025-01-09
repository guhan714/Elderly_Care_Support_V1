namespace ElderlyCareSupport.Application.IService
{
    public interface IForgotPaswordService
    {
        Task<string?> GetForgotPassword(string userName);
    }
}
