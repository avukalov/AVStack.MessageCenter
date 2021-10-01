using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AVStack.IdentityServer.Common.Models;
using AVStack.MessageCenter.Models;
using MimeKit;

namespace AVStack.MessageCenter.Common.AutoMapper
{
    public class EmailProfile : Profile
    {
        public EmailProfile()
        {
            CreateMap<IdentityMessage, UserRegistrationEmailModel>()
                .ForMember(
                    dest => dest.ConfirmationLink,
                    m =>
                        m.MapFrom(s => s.Callback))
                .ForMember(
                    dest => dest.To,
                    m =>
                        m.MapFrom(x => MapTo(new string[] {x.EmailAddress})));
        }

        private object MapTo(IEnumerable<string> emailAddresses)
        {
            var to = new List<MailboxAddress>();
            to.AddRange(emailAddresses.Select(MailboxAddress.Parse));
            return to;
        }
    }
}