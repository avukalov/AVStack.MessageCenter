using System.Threading.Tasks;
using AVStack.MessageCenter.Models;
using MimeKit;
using RabbitMQ.Client.Events;

namespace AVStack.MessageCenter.Services.Interfaces
{
    public interface IEmailService
    {
       Task SendEmailConfirmation(EmailConfirmationModel model);
    }
}