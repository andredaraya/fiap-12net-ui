using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace GeekBurger.Ui.Application.ServiceBus
{
    public interface IReceiveMessagesService
    {
        string _subscription { get; set; }
        string _topic { get; set; }

        void EnsureSubscriptionsIsCreated();
        void EnsureTopicIsCreated();
        Task Handle(Message message, CancellationToken arg2);
    }
}