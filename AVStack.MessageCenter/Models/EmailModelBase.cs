using System;
using System.Collections.Generic;
using System.Linq;
using AVStack.MessageCenter.Models.Interfaces;
using MimeKit;

namespace AVStack.MessageCenter.Models
{
    public abstract class EmailModelBase : IEmailModel
    {
        public string MessageType { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
        public List<MailboxAddress> To { get; set; }

        protected EmailModelBase()
        {
        }

        protected EmailModelBase(IEnumerable<string> to)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(MailboxAddress.Parse));
        }
    }
}