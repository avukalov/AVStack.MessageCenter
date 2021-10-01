using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AVStack.IdentityServer.Common.Models;
using AVStack.MessageCenter.Common.Configuration;
using AVStack.MessageCenter.Models;
using AVStack.MessageCenter.Models.Interfaces;
using AVStack.MessageCenter.Services.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace AVStack.MessageCenter.Services
{
    public abstract class EmailServiceBase : IEmailService
    {
        private readonly EmailConfigurationOptions _emailOptions;
        private readonly EmailTemplatesOptions _templateOptions;
        protected EmailServiceBase(
            IOptions<EmailConfigurationOptions> emailOptions,
            IOptions<EmailTemplatesOptions> templateOptions)
        {
            _emailOptions = emailOptions.Value;
            _templateOptions = templateOptions.Value;
        }

        public virtual async Task AppendDataToHtmlTemplate(string templateType, MimeMessage email, params object[] templateData)
        {
            var htmlTemplate = await GetHtmlTemplateAsync(templateType);
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = string.Format(htmlTemplate, templateData)
            };
        }

        [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
        public MimeMessage CreateEmail(IEmailModel emailModel)
        {
            var mimeMessage = new MimeMessage();

            mimeMessage.Subject = emailModel.Subject;
            mimeMessage.To.AddRange(emailModel.To);

            // TODO: Implement logic to assign different emails for sending
            // TODO: Think about how to add From field to IEmailModel interface
            mimeMessage.From.Add(new MailboxAddress(string.Empty, _emailOptions.From));

            return mimeMessage;
        }

        public Task SendAsync(MimeMessage email)
        {
            return SendEmailAsync(email);
        }

        // private Task<string> GetHtmlTemplateAsync(string templateType)
        // {
        //     return templateType switch
        //     {
        //         nameof(UserRegistration) => LoadHtmlTemplateFromFileAsync(_templateOptions.BasePath + _templateOptions.EmailConfirmation),
        //         _ => Task.FromResult(string.Empty)
        //     };
        // }

        private async Task<string> GetHtmlTemplateAsync(string templateName)
        {
            var templatePath = _templateOptions.BasePath + $"{templateName}Template.html";

            using (var streamReader = File.OpenText(templatePath))
            {
                return await streamReader.ReadToEndAsync();
            }
        }

        private async Task SendEmailAsync(MimeMessage message)
        {
            using (var smtpClient = new SmtpClient())
            {
                // TODO: Add authentication for email server
                try
                {
                    await smtpClient.ConnectAsync(_emailOptions.SmtpServer, _emailOptions.Port, _emailOptions.SslEnabled);
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