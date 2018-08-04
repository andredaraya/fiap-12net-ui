using GeekBurger.Ui.Application.Options;
using GeekBurger.Ui.Application.ServiceBus.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekBurger.Ui.Application.ServiceBus
{
    public class ReceiveMessagesService
    {
        private readonly string _topicName;
        private readonly string _subscriptionName;
        private readonly ServiceBusOptions _configuration;
        private readonly IHubContext<MessageHub> _hubContext;
        private readonly ILogger<ReceiveMessagesService> _logger;

        public ReceiveMessagesService(IOptions<ServiceBusOptions> configuration, IHubContext<MessageHub> hubContext, ILogger<ReceiveMessagesService> logger,
            string topic, string subscription, string filterName = null, string filter = null)
        {
            _logger = logger;
            _hubContext = hubContext;
            _configuration = configuration.Value;
            _topicName = topic;
            _subscriptionName = subscription;

            ReceiveMessages(filterName, filter);
        }

        private void ReceiveMessages(string filterName = null, string filter = null)
        {
            var subscriptionClient = new SubscriptionClient
                (_configuration.ConnectionString, _topicName, _subscriptionName);

            var mo = new MessageHandlerOptions(ExceptionHandle) { AutoComplete = true };

            if (filterName != null && filter != null)
            {
                const string defaultRule = "$default";

                if (subscriptionClient.GetRulesAsync().Result.Any(x => x.Name == defaultRule))
                    subscriptionClient.RemoveRuleAsync(defaultRule).Wait();

                if (subscriptionClient.GetRulesAsync().Result.All(x => x.Name != filterName))
                    subscriptionClient.AddRuleAsync(new RuleDescription
                    {
                        Filter = new CorrelationFilter { Label = filter },
                        Name = filterName
                    }).Wait();

            }

            subscriptionClient.RegisterMessageHandler(Handle, mo);
        }

        private Task Handle(Message message, CancellationToken arg2)
        {
            var messageString = "";
            if (message.Body != null)
                messageString = Encoding.UTF8.GetString(message.Body);

            //TODO: be more generic            
            switch (message.Label.ToLower())
            {
                case "storecatalogready":
                    var storeCatalogReady = JsonConvert.DeserializeObject<StoreCatalogReadyMessage>(messageString);
                    _hubContext.Clients.All.SendAsync(_topicName, message.Label, storeCatalogReady ?? (object)messageString);
                    break;
                case "userretrieved":
                    var userRetrieved = JsonConvert.DeserializeObject<UserRetrievedMessage>(messageString);
                    _hubContext.Clients.All.SendAsync(_topicName, message.Label, userRetrieved ?? (object)messageString);
                    break;
                default:
                    return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }

        private Task ExceptionHandle(ExceptionReceivedEventArgs arg)
        {
            _logger.LogError($"Message handler encountered an exception {arg.Exception}.");
            var context = arg.ExceptionReceivedContext;
            _logger.LogError($"- Endpoint: {context.Endpoint}, Path: {context.EntityPath}, Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
