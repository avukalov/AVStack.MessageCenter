using System.Threading.Tasks;
using AVStack.MessageBus.Abstraction;
using AVStack.MessageCenter.Models.Interfaces;
using MimeKit;
using RabbitMQ.Client.Events;

namespace AVStack.MessageCenter.Services.Interfaces
{
    public interface IEmailService
    {
        Task AppendDataToHtmlTemplate(MimeMessage email, string templateType, params object[] templateData);
        MimeMessage CreateEmail(IEmailModel emailModel, string subject);
        Task SendAsync(MimeMessage email);
    }
}