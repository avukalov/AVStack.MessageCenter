using AVStack.MessageCenter.Common.Configuration;
using AVStack.MessageCenter.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace AVStack.MessageCenter.Services
{
    public class EmailService : EmailServiceBase, IEmailService
    {
        public EmailService(
            IOptions<EmailConfigurationOptions> emailOptions,
            IOptions<EmailTemplatesOptions> templateOptions)
            : base(emailOptions, templateOptions)
        {
        }

    }
}