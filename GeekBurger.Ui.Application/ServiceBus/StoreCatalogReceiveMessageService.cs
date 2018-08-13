using GeekBurger.Ui.Application.Options;
using GeekBurger.Ui.Application.ServiceBus.Models;
using GeekBurger.Ui.Contracts.Messages;
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
        private readonly IStoreCatalogService _storeCatalogService;

        public override string _topic { get; set; } = "StoreCatalogReady";
        public override string _subscription { get; set; } = "UI";
        private Guid? STORE_ID;

        public StoreCatalogReceiveMessageService(IOptions<ServiceBusOptions> serviceBusOptions, IStoreCatalogService storeCatalogService, ILogger logger, IUIServiceBus uiServiceBus)
            : base(serviceBusOptions, logger)
        {
            _serviceBusOptions = serviceBusOptions;
            _logger = logger;
            _uiServiceBus = uiServiceBus;
            _storeCatalogService = storeCatalogService;
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
                STORE_ID = storeCatalogReady.StoreId;
                _uiServiceBus.AddToMessageList(new ShowWelcomePageMessage(), "ShowWelcomePage");
                _uiServiceBus.SendMessagesAsync("UICommand");
            }

            return Task.CompletedTask;
        }

        public override void EnsureSubscriptionsIsCreated()
        {
            base.EnsureSubscriptionsIsCreated();

            //if not ready, id is null
            if (this._storeCatalogService != null)
                STORE_ID = this._storeCatalogService.GetStoreCatalog().Result;
        }

        public Guid GetStoreId()
        {
            return STORE_ID.Value;
        }
    }
}
