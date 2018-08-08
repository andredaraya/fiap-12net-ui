using GeekBurger.Ui.Application.Options;
using Microsoft.Azure.Management.ServiceBus.Fluent;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeekBurger.Ui.Application.ServiceBus
{
    public abstract class ReceiveMessagesService : IReceiveMessagesService
    {
        private readonly ServiceBusOptions _configuration;
        private readonly ILogger _logger;
        private readonly IServiceBusNamespace _namespace;
        public abstract string _topic { get; set; }
        public abstract string _subscription { get; set; }

        public ReceiveMessagesService(IOptions<ServiceBusOptions> configuration, ILogger logger)
        {
            _logger = logger;
            _configuration = configuration.Value;
            _namespace = ServiceBusNamespaceExtension.GetServiceBusNamespace(_configuration);

            EnsureTopicIsCreated();
            EnsureSubscriptionsIsCreated();
        }

        public void EnsureTopicIsCreated()
        {
            if (!_namespace.Topics.List().Any(topic => topic.Name.Equals(_topic, StringComparison.InvariantCultureIgnoreCase)))
                _namespace.Topics.Define(_topic).WithSizeInMB(1024).Create();
        }

        public void EnsureSubscriptionsIsCreated()
        {
            var topic = _namespace.Topics.GetByName(_topic);

            if (!topic.Subscriptions.List()
                                    .Any(subscription => subscription.Name.Equals(_subscription,
                                                                                  StringComparison.InvariantCultureIgnoreCase)))
            {
                topic.Subscriptions.Define(_subscription).Create();
            }
        }

        private void ReceiveMessages(string filterName = null, string filter = null)
        {
            var subscriptionClient = new SubscriptionClient
                (_configuration.ConnectionString, _topic, _subscription);

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

        public abstract Task Handle(Message message, CancellationToken arg2);

        private Task ExceptionHandle(ExceptionReceivedEventArgs arg)
        {
            _logger.LogError($"Message handler encountered an exception {arg.Exception}.");
            var context = arg.ExceptionReceivedContext;
            _logger.LogError($"- Endpoint: {context.Endpoint}, Path: {context.EntityPath}, Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
