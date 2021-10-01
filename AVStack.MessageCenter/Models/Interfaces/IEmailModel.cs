using System;
using System.Collections.Generic;
using MimeKit;

namespace AVStack.MessageCenter.Models.Interfaces
{
    public interface IEmailModel
    {
        string MessageType { get; set; }
        string Subject { get; set; }
        string From { get; set; }
        public List<MailboxAddress> To { get; set; }
    }
}