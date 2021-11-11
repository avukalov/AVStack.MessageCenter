using System;
using System.Threading;
using System.Threading.Tasks;
using AVStack.MessageBus.Abstraction;
using AVStack.MessageBus.Extensions;
using AVStack.MessageBus.RabbitMQ;
using AVStack.MessageCenter.Services.Interfaces;
using Microsoft.Extensions.Hosting;

namespace AVStack.MessageCenter.Hosts
{
    public class MessageCenterBgService : BackgroundService
    {
        public string ConsumerTag { get; set; }
        public string Queue => "message-center";

        private readonly IMessageBusFactory _busFactory;
        private readonly ITopicService _topicService;

        public MessageCenterBgService(
            IMessageBusFactory busFactory,
            ITopicService topicService)
        {
            _busFactory = busFactory;
            _topicService = topicService;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var consumer = _busFactory.CreateConsumer();

            ConsumerTag = consumer.ConsumeAsync(Queue, async (model, ea) =>
            {
                try
                {
                    await _topicService.HandleAsync(ea);

                    consumer.BasicAck(ea.DeliveryTag);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e); throw new Exception(e.Message);
                }
            });

            return Task.FromResult(0);
        }
    }
}