using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVStack.MessageCenter.Handlers;
using AVStack.MessageCenter.Services.Interfaces;
using RabbitMQ.Client.Events;

namespace AVStack.MessageCenter.Services
{
    public class TopicService : ITopicService
    {
        private readonly IEnumerable<ITopicHandler> _handlers;

        public TopicService(IEnumerable<ITopicHandler> handlers)
        {
            _handlers = handlers;
        }

        public async Task HandleAsync(BasicDeliverEventArgs args)
        {
            if (_handlers != null && _handlers.Any())
            {
                var supportedHandlers = _handlers
                    .Where(x => x.Topic.Equals(args.RoutingKey))
                    .ToList();

                if (supportedHandlers.Any())
                {
                    var handlingTasks = supportedHandlers
                        .Select(x => x.HandleAsync(Encoding.UTF8.GetString(args.Body.ToArray())))
                        .ToList();

                    if (handlingTasks.Any())
                    {
                        await Task.WhenAll(handlingTasks);
                    }
                }
            }
        }
    }
}