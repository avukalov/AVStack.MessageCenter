using System.Collections.Generic;
using System.Threading.Tasks;

namespace AVStack.MessageCenter.Handlers
{
    public interface ITopicHandler
    {
        string Topic { get; }

        Task HandleAsync(string body);
    }
}