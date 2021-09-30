using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AVStack.MessageCenter.Common.Configuration;
using AVStack.MessageCenter.Models;
using AVStack.MessageCenter.Services.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace AVStack.MessageCenter.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions _emailOptions;

        public EmailService(IOptions<EmailOptions> emailOptions)
        {
            _emailOptions = emailOptions.Value;
        }

        public async Task SendEmailConfirmation(EmailConfirmationModel model)
        {

            var emailModel = new EmailMessageModel(new string[] { model.Email },"Email confirmation");

            var mimeMessage = CreateMimeMessage(emailModel);
            await AppendHtmlTemplate(mimeMessage, model);

            Console.WriteLine(mimeMessage.Body);
        }

        private async Task AppendHtmlTemplate(MimeMessage mimeMessage, EmailConfirmationModel model)
        {
            var templatePath = $"{Directory.GetCurrentDirectory()}/Common/Templates/EmailConfirmationTemplate.html";

            using (var streamReader = System.IO.File.OpenText(templatePath))
            {
                var htmlTemplate = await streamReader.ReadToEndAsync();
                mimeMessage.Body = new TextPart(TextFormat.Html)
                {
                    Text = string.Format(htmlTemplate, model.FirstName, model.LastName, model.ConfirmationLink)
                };
            }
        }

        private MimeMessage CreateMimeMessage(EmailMessageModel emailModel)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(string.Empty, _emailOptions.From));
            mimeMessage.To.AddRange(emailModel.To);
            mimeMessage.Subject = emailModel.Subject;
            // emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            return mimeMessage;
        }

        private Task SendAsync(MimeMessage message)
        {
            return SendEmailAsync(message);
        }

        private async Task SendEmailAsync(MimeMessage message)
        {
            using (var smtpClient = new SmtpClient())
            {
                try
                {
                    await smtpClient.ConnectAsync(_emailOptions.SmtpServer, _emailOptions.Port, _emailOptions.SslEnabled);
                    // smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
                    // smtpClient.Authenticate(_emailOptions.UserName, _emailOptions.Password);

                    await smtpClient.SendAsync(message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    await smtpClient.DisconnectAsync(true);
                    smtpClient.Dispose();
                }
            }
        }
    }
}