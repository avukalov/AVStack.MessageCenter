using System;
using AVStack.MessageCenter.Common.Configuration;
using AVStack.MessageCenter.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace AVStack.MessageCenter.Services
{
    public class EmailServiceFactory : IEmailServiceFactory
    {
        private readonly IOptions<EmailConfigurationOptions> _emailOptions;
        private readonly IOptions<EmailTemplatesOptions> _templatesOptions;

        public EmailServiceFactory(
            IOptions<EmailConfigurationOptions> emailOptions,
            IOptions<EmailTemplatesOptions> templatesOptions)
        {
            _emailOptions = emailOptions;
            _templatesOptions = templatesOptions;
        }

        public T Create<T>() where T : class
        {
            var constructorInfo = typeof(T).GetConstructor(new[] { typeof(IOptions<EmailConfigurationOptions>), typeof(IOptions<EmailTemplatesOptions>) });
            if (constructorInfo == null)
            {
                throw new InvalidOperationException($"A constructor for type '{typeof(T)}' was not found.");
            }
            return (T)constructorInfo.Invoke(new object[] { _emailOptions, _templatesOptions});
        }
    }
}