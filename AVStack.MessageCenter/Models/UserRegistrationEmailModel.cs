using System.Collections.Generic;
using System.Linq;
using AVStack.MessageCenter.Models.Interfaces;
using MimeKit;

namespace AVStack.MessageCenter.Models
{
    public class UserRegistrationEmailModel : EmailModelBase, IUserRegistrationEmailModel
    {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string ConfirmationLink { get; set; }

        public UserRegistrationEmailModel() : base()
        {
        }
        public UserRegistrationEmailModel(IEnumerable<string> to) : base (to)
        {
        }
    }
}