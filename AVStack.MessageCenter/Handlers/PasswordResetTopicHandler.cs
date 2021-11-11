using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using AVStack.IdentityServer.Common.Models;
using AVStack.MessageCenter.Models;
using AVStack.MessageCenter.Services.Interfaces;


namespace AVStack.MessageCenter.Handlers
{
    public class PasswordResetTopicHandler : TopicHandlerBase, ITopicHandler
    {
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public PasswordResetTopicHandler(
            IEmailService emailService,
            IMapper mapper)
        {
            _emailService = emailService;
            _mapper = mapper;
        }

        public string Topic => "account.user.password-reset";

        public override async Task HandleAsync(string body)
        {
            var emailModel = _mapper.Map<EmailModel>(JsonSerializer.Deserialize<PasswordReset>(body));

            var mimeMessage = _emailService.CreateEmail(emailModel, "AVStack Accounts - Password reset");
            await _emailService.AppendDataToHtmlTemplate(mimeMessage, nameof(PasswordReset), new object[]
            {
                emailModel.FullName,
                emailModel.Callback
            });

            await _emailService.SendAsync(mimeMessage);
        }
    }
}