using System.Collections.Generic;
using System.Linq;
using AVStack.MessageCenter.Models.Interfaces;
using MimeKit;

namespace AVStack.MessageCenter.Extensions
{
    public static class Helpers
    {
        public static void Format(this IEmailModel emailModel)
        {
            emailModel.To = new List<MailboxAddress>();
            emailModel.To.AddRange(new []{ emailModel.EmailAddress }.Select(MailboxAddress.Parse));
        }
    }
}