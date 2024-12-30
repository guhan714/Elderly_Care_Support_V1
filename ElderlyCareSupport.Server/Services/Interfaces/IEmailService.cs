using SendGrid;
using SendGridMessage = SendGrid.Helpers.Mail.SendGridMessage;

namespace ElderlyCareSupport.Server.Services.Interfaces;

public interface IEmailService
{
    Task<Tuple<SendGridClient, SendGridMessage>> ConfigureEmailService(string recipient, string userName);
    Task SendEmailAsync(string recipient, string userName);
}