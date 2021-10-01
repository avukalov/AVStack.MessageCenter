using System.Threading.Tasks;
using AVStack.MessageCenter.Models;
using AVStack.MessageCenter.Models.Interfaces;
using MimeKit;

namespace AVStack.MessageCenter.Services.Interfaces
{
    public interface IIdentityEmailService : IEmailService
    {
        Task HandleAsync(IUserRegistrationEmailModel email);
    }
}