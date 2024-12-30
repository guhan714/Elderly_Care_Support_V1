﻿using ElderlyCareSupport.Server.Common;
using ElderlyCareSupport.Server.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using SendGrid;
using SendGrid.Helpers.Mail;
using SendGridMessage = SendGrid.Helpers.Mail.SendGridMessage;

namespace ElderlyCareSupport.Server.Helpers;

public class EmailHelper(IConfiguration configuration) : IEmailService
{
    private readonly IConfiguration _configuration = configuration;
    public async Task<Tuple<SendGridClient, SendGridMessage>> ConfigureEmailService(string recipient, string userName)
    {
        var apiKey = _configuration["SendGridAPI"]!;
        var client = new SendGridClient(apiKey);

        EmailAddress from = new(CommonConstants.SenderEmailAddress, CommonConstants.SenderNamePlaceHolder);
        const string subject = CommonConstants.EmailSubject;
        var to = new EmailAddress(recipient, "Recipient Name");
        const string plainTextContent = "This is a test email.";
        var htmlContent = await File.ReadAllTextAsync(CommonConstants.RegistrationMailContentPath);
        htmlContent = htmlContent.Replace("{{UserName}}", userName);

        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        return Tuple.Create(client, msg);
    }

    public async Task SendEmailAsync(string recipient, string userName)
    {
        try
        {
            var mailConfiguration = await ConfigureEmailService(recipient, userName);
            var response = await mailConfiguration.Item1.SendEmailAsync(mailConfiguration.Item2);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}