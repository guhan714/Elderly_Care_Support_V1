using SendGrid;
using SendGridMessage = SendGrid.Helpers.Mail.SendGridMessage;

namespace ElderlyCareSupport.Application.IService;

public interface IEmailService
{
    Task<Tuple<SendGridClient, SendGridMessage>> ConfigureEmailService(string recipient, string userName);
    Task<bool> SendEmailAsync(string recipient, string userName);
}