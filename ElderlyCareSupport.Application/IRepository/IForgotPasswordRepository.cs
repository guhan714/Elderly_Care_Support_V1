namespace ElderlyCareSupport.Application.IRepository
{
    public interface IForgotPasswordRepository
    {
        Task<string?> GetPasswordAsync(string userName);
    }
}
