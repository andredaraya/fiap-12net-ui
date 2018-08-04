using GeekBurger.Ui.Application.Options;
using GeekBurger.Ui.Application.ServiceBus.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Ui.Application.ServiceBus
{
    public class ReceiveMessagesFactory : IReceiveMessagesFactory
    {
        private readonly IOptions<ServiceBusOptions> _configuration;
        private readonly IHubContext<MessageHub> _hubContext;
        private readonly ILogger<ReceiveMessagesService> _logger;

        public ReceiveMessagesFactory(IOptions<ServiceBusOptions> configuration, IHubContext<MessageHub> hubContext, ILogger<ReceiveMessagesService> logger)
        {
            _configuration = configuration;
            _hubContext = hubContext;
            _logger = logger;

            CreateNew("storecatalogready", "ui");
            CreateNew("userretrieved", "ui");
        }

        public ReceiveMessagesService CreateNew(string topic, string subscription, string filterName = null, string filter = null)
        {
            return new ReceiveMessagesService(_configuration, _hubContext, _logger, topic, subscription, filterName, filter);
        }
    }
}
