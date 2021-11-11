using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AVStack.IdentityServer.Common.Models;
using AVStack.MessageCenter.Models;
using MimeKit;

namespace AVStack.MessageCenter
{
    public class MappingConfigurationSection : Profile
    {
        public MappingConfigurationSection()
        {
            CreateMap<UserRegistration, EmailModel>()
                .ForMember(dest => dest.To, m =>
                    m.MapFrom(x => MapTo(new[] {x.EmailAddress})));

            CreateMap<PasswordReset, EmailModel>()
                .ForMember(dest => dest.To, m =>
                    m.MapFrom(x => MapTo(new[] {x.EmailAddress})));
        }

        private object MapTo(IEnumerable<string> emailAddresses)
        {
            var to = new List<MailboxAddress>();
            to.AddRange(emailAddresses.Select(MailboxAddress.Parse));
            return to;
        }
    }
}