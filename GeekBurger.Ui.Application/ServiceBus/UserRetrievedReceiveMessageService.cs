using GeekBurger.Ui.Application.Options;
using GeekBurger.Ui.Application.ServiceBus.Models;
using GeekBurger.Ui.Contracts.Messages;
using GeekBurger.Ui.Domain.Interface;
using GeekBurger.Ui.Domain.Request;
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
        private readonly IStoreCatalogService _storeCatalogService;

        public override string _subscription { get; set; } = "UI";
        public override string _topic { get; set; } = "UserRetrieved";
        public Guid RequesterId { get; set; }

        public UserRetrievedReceiveMessageService(IOptions<ServiceBusOptions> serviceBusOptions, ILogger logger, IUIServiceBus serviceBus, IStoreCatalogService storeCatalogService)
            : base(serviceBusOptions, logger)
        {
            _serviceBus = serviceBus;
            _storeCatalogService = storeCatalogService;
        }

        public override Task Handle(Message message, CancellationToken arg2)
        {
            var messageString = "";
            if (message.Body != null)
                messageString = Encoding.UTF8.GetString(message.Body);


            var userRetrieved = JsonConvert.DeserializeObject<UserRetrievedMessage>(messageString);

            if (userRetrieved.AreRestrictionsSet)
            {
                GetProductsRequest request = new GetProductsRequest()
                {
                    StoreName = "Los Angeles - Pasadena"
                };

                var response = _storeCatalogService.GetProducts(request).Result;

                _serviceBus.AddToMessageList(new ShowProductsListMessage() { Products = response.ConvertToMessageProduct(response.Products, this.RequesterId) }, "ShowProductsList");
                _serviceBus.SendMessagesAsync("UICommand");
            }
            else
            {
                _serviceBus.AddToMessageList(new ShowFoodRestrictionsFormMessage() { UserId = userRetrieved.UserId, RequesterId = this.RequesterId }, "ShowFoodRestrictionsForm");
                _serviceBus.SendMessagesAsync("UICommand");
            }

            return Task.CompletedTask;
        }
    }
}
