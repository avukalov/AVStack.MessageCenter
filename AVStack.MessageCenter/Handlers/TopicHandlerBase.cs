using System;
using System.Threading.Tasks;
using AVStack.MessageBus.Abstraction;
using RabbitMQ.Client.Events;

namespace AVStack.MessageCenter.Handlers
{
    public abstract class TopicHandlerBase
    {
        public abstract Task HandleAsync(string body);
    }
}