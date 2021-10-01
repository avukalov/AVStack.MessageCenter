using System.Threading.Tasks;
using AVStack.MessageCenter.Models;
using AVStack.MessageCenter.Models.Interfaces;
using MimeKit;
using RabbitMQ.Client.Events;

namespace AVStack.MessageCenter.Services.Interfaces
{
    public interface IEmailService
    {
        Task AppendDataToHtmlTemplate(string templateType, MimeMessage email, params object[] templateData);
        MimeMessage CreateEmail(IEmailModel emailModel);
        Task SendAsync(MimeMessage email);
    }
}