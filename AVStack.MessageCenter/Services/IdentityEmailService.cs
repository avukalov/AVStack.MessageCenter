using System;
using System.Threading.Tasks;
using AVStack.IdentityServer.Common.Models;
using AVStack.IdentityServer.Constants;
using AVStack.MessageCenter.Common.Configuration;
using AVStack.MessageCenter.Models.Interfaces;
using AVStack.MessageCenter.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace AVStack.MessageCenter.Services
{
    public class IdentityEmailService : EmailServiceBase, IIdentityEmailService
    {
        public IdentityEmailService(
            IOptions<EmailConfigurationOptions> emailOptions,
            IOptions<EmailTemplatesOptions> templatesOptions
        ) : base(emailOptions, templatesOptions)
        {

        }

        public async Task HandleAsync(IUserRegistrationEmailModel emailModel)
        {
            var email = CreateEmail(emailModel);

            switch (emailModel.MessageType)
            {
                // TODO: Change logic for nameof()
                case nameof(IdentityMessageTypes.UserRegistration):
                    await AppendDataToHtmlTemplate(emailModel.MessageType, email, new object[] {emailModel.FullName, emailModel.ConfirmationLink});
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(emailModel.MessageType), emailModel.MessageType, "MessageType can not be null");
            }

            await SendAsync(email);
        }
    }
}