namespace ElderlyCareSupport.Server.Services.Implementations;

public interface IEmailService
{
    Task SendEmailAsync(string recipient);
}