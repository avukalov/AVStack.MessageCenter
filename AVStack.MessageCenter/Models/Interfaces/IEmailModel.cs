using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MimeKit;

namespace AVStack.MessageCenter.Models.Interfaces
{
    public interface IEmailModel
    {
        string FullName { get; set; }
        string EmailAddress { get; set; }
        string Callback { get; set; }
        string Subject { get; set; }
        string From { get; set; }
        public List<MailboxAddress> To { get; set; }
    }
}