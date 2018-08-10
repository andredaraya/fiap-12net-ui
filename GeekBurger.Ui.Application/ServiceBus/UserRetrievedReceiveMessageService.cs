using GeekBurger.Ui.Application.Options;
using GeekBurger.Ui.Application.ServiceBus.Models;
using GeekBurger.Ui.Domain.Interface;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekBurger.Ui.Application.ServiceBus
{
    public class UserRetrievedReceiveMessageService : ReceiveMessagesService, IUserRetrievedReceiveMessageService
    {
        private readonly IUIServiceBus _serviceBus;
        private readonly IOptions<ServiceBusOptions> _serviceBusOptions;
        private readonly ILogger _logger;

        public override string _subscription { get; set; }
        public override string _topic { get; set; }
        public Guid RequesterId { get; set; }

        public UserRetrievedReceiveMessageService(IOptions<ServiceBusOptions> serviceBusOptions, ILogger logger, IUIServiceBus serviceBus)
            : base(serviceBusOptions, logger)
        {
            _serviceBus = serviceBus;
            _serviceBusOptions = serviceBusOptions;
            _logger = logger;

            RequesterId = Guid.NewGuid();
        }

        public override Task Handle(Message message, CancellationToken arg2)
        {
            var messageString = "";
            if (message.Body != null)
                messageString = Encoding.UTF8.GetString(message.Body);


            var userRetrieved = JsonConvert.DeserializeObject<UserRetrievedMessage>(messageString);

            if (userRetrieved.AreRestrictionsSet)
            {
                
            }
            else
            {
                _serviceBus.AddToMessageList(new ShowFoodRestrictionsFormMessage() { UserId = userRetrieved.UserId, RequesterId = RequesterId  }, "ShowWelcomePage");
                _serviceBus.SendMessagesAsync();
            }

            return Task.CompletedTask;
        }
    }
}
