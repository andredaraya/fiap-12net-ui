using GeekBurger.Ui.Application.Options;
using GeekBurger.Ui.Application.ServiceBus.Models;
using GeekBurger.Ui.Domain.Interface;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekBurger.Ui.Application.ServiceBus
{
    public class StoreCatalogReceiveMessageService : ReceiveMessagesService, IStoreCatalogReceiveMessageService
    {
        private readonly IOptions<ServiceBusOptions> _serviceBusOptions;
        private readonly ILogger _logger;
        private readonly IUIServiceBus _uiServiceBus;

        public override string _topic { get; set; }
        public override string _subscription { get; set; }

        public StoreCatalogReceiveMessageService(IOptions<ServiceBusOptions> serviceBusOptions, ILogger logger, IUIServiceBus uiServiceBus)
            : base(serviceBusOptions, logger)
        {
            _serviceBusOptions = serviceBusOptions;
            _logger = logger;
            _uiServiceBus = uiServiceBus;
        }

        public override Task Handle(Message message, CancellationToken arg2)
        {
            var messageString = "";
            if (message.Body != null)
                messageString = Encoding.UTF8.GetString(message.Body);

            //TODO: be more generic            
            var storeCatalogReady = JsonConvert.DeserializeObject<StoreCatalogReadyMessage>(messageString);

            if (storeCatalogReady.Ready)
            {
                _uiServiceBus.AddToMessageList(new ShowWelcomePageMessage(), "ShowWelcomePage");
                _uiServiceBus.SendMessagesAsync();
            }

            return Task.CompletedTask;
        }
    }
}
