using System.Collections.Generic;
using System.Linq;
using MimeKit;

namespace AVStack.MessageCenter.Models
{
    public class EmailMessageModel
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }

        public EmailMessageModel(IEnumerable<string> to, string subject)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(MailboxAddress.Parse));
            Subject = subject;
        }
    }
}