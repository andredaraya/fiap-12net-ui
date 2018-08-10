using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace GeekBurger.Ui.Application.ServiceBus
{
    public interface IUserRetrievedReceiveMessageService
    {
        string _subscription { get; set; }
        string _topic { get; set; }
        Guid RequesterId { get; set; }

        Task Handle(Message message, CancellationToken arg2);
    }
}