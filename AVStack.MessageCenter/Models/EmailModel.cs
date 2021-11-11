using System.Collections.Generic;
using System.Linq;
using AVStack.MessageCenter.Models.Interfaces;
using MimeKit;

namespace AVStack.MessageCenter.Models
{
    public class EmailModel : IEmailModel
    {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Callback { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
        public List<MailboxAddress> To { get; set; }
    }
}