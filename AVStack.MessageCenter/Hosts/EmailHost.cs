using System;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal;
using AVStack.IdentityServer.Common.Models;
using AVStack.MessageBus.Abstraction;
using AVStack.MessageBus.Extensions;
using AVStack.MessageCenter.Models;
using AVStack.MessageCenter.Services;
using AVStack.MessageCenter.Services.Interfaces;
using Microsoft.Extensions.Hosting;

namespace AVStack.MessageCenter.Hosts
{
    public class EmailHost : BackgroundService
    {
        private readonly IMessageBusFactory _busFactory;
        private readonly IEmailServiceFactory _emailServiceFactory;
        private readonly IMapper _mapper;

        public EmailHost(
            IMessageBusFactory busFactory,
            IEmailServiceFactory emailServiceFactory,
            IMapper mapper)
        {
            _busFactory = busFactory;
            _emailServiceFactory = emailServiceFactory;
            _mapper = mapper;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var emailConsumer = _busFactory.CreateConsumer();

            emailConsumer.ConsumeAsync("email", async (model, ea) =>
            {
                try
                {

                    var emailConfirmationModel = Serializer.Deserialize<IdentityMessage>(ea.Body, ea.BasicProperties.ContentType);

                    var emailModel = _mapper.Map<UserRegistrationEmailModel>(emailConfirmationModel);

                    emailModel.MessageType = Encoding.UTF8.GetString((byte[])ea.BasicProperties.Headers.GetOrDefault("message-type"));

                    var identityEmailService = _emailServiceFactory.Create<IdentityEmailService>();
                    await identityEmailService.HandleAsync(emailModel);

                    emailConsumer.BasicAck(ea.DeliveryTag);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                await Task.FromResult(true);
            });

            return Task.CompletedTask;
        }
    }
}