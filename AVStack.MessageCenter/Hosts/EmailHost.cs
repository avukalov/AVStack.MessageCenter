using System;
using System.Threading;
using System.Threading.Tasks;
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
        private readonly IEmailService _emailService;

        public EmailHost(IMessageBusFactory busFactory, IEmailService emailService)
        {
            _busFactory = busFactory;
            _emailService = emailService;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var emailConsumer = _busFactory.CreateConsumer();

            emailConsumer.ConsumeAsync("email", async (model, ea) =>
            {
                try
                {
                    var emailConfirmationModel = Serializer.Deserialize<EmailConfirmationModel>(ea.Body, ea.BasicProperties.ContentType);

                    await _emailService.SendEmailConfirmation(emailConfirmationModel);

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