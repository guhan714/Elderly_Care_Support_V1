using ElderlyCareSupport.Server.Services.Implementations;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace ElderlyCareSupport.Server.Helpers;

public class EmailHelper(IConfiguration configuration): IEmailService
{
    public async Task SendEmailAsync(string recipient)
    {
        var apiKeySettings = configuration.GetSection("SendGridAPI");
        var apiKey = apiKeySettings["ApiKey"]; // Replace with your API key
        var client = new SendGridClient(apiKey);

        var from = new EmailAddress("guhan000714@gmail.com", "Sender Name");
        var subject = "Test Email from SendGrid";
        var to = new EmailAddress(recipient, "Recipient Name");
        var plainTextContent = "This is a test email.";
        var htmlContent = "<strong>This is a test email.</strong>";

        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

        try
        {
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine($"Status Code: {response.StatusCode}");

            var responseBody = await response.Body.ReadAsStringAsync();
            Console.WriteLine($"Response Body: {responseBody}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}