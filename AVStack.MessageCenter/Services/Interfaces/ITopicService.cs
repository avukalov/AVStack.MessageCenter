using System.Threading.Tasks;
using AVStack.MessageBus.Abstraction;
using RabbitMQ.Client.Events;

namespace AVStack.MessageCenter.Services.Interfaces
{
    public interface ITopicService
    {
        // Task AppendDataToHtmlTemplate(MimeMessage email, string templateType, params object[] templateData);
        // MimeMessage CreateEmail(IEmailModel emailModel, string subject);
        Task HandleAsync(BasicDeliverEventArgs args);
    }
}