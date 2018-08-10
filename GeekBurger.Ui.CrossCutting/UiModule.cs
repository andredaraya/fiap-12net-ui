using Autofac;
using GeekBurger.Ui.Application.ExternalServices;
using GeekBurger.Ui.Application.Options;
using GeekBurger.Ui.Application.ServiceBus;
using GeekBurger.Ui.Domain.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GeekBurger.Ui.CrossCutting
{
    public class UiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            this.RegisterServices(builder);
            base.Load(builder);
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            #region [Order Service]
            builder.Register(type => new OrderOptions(type.Resolve<IConfiguration>()))
                    .As<OrderOptions>()
                    .SingleInstance()
                    .AutoActivate();

            builder.Register(type => new OrderService(type.Resolve<OrderOptions>()))
                .As<IOrderService>()
                .SingleInstance()
                .AutoActivate();
            #endregion

            #region [Store Catalog Service]
            builder.Register(type => new StoreCatalogOptions(type.Resolve<IConfiguration>()))
                    .As<StoreCatalogOptions>()
                    .SingleInstance()
                    .AutoActivate();

            builder.Register(type => new StoreCatalogService(type.Resolve<StoreCatalogOptions>()))
                .As<IStoreCatalogService>()
                .SingleInstance()
                .AutoActivate();
            #endregion

            #region [User Service]
            builder.Register(type => new UserOptions(type.Resolve<IConfiguration>()))
                    .As<UserOptions>()
                    .SingleInstance()
                    .AutoActivate();

            builder.Register(type => new UserService(type.Resolve<UserOptions>()))
                .As<IUserService>()
                .SingleInstance()
                .AutoActivate();
            #endregion

            #region [Service Bus]
            builder.Register(type => new UIServiceBus(type.Resolve<IOptions<ServiceBusOptions>>()))
                .As<IUIServiceBus>()
                .SingleInstance()
                .AutoActivate();
            #endregion

            builder.Register(type => new UserRetrievedReceiveMessageService(type.Resolve<IOptions<ServiceBusOptions>>(), type.Resolve<ILogger<UserRetrievedReceiveMessageService>>(), type.Resolve<IUIServiceBus>(), type.Resolve<IStoreCatalogService>()))
              .As<IUserRetrievedReceiveMessageService>()
              .SingleInstance()
              .AutoActivate();

            builder.Register(type => new StoreCatalogReceiveMessageService(type.Resolve<IOptions<ServiceBusOptions>>(), type.Resolve<IStoreCatalogService>(), type.Resolve<ILogger<StoreCatalogReceiveMessageService>>(), type.Resolve<IUIServiceBus>()))
              .As<IStoreCatalogReceiveMessageService>()
              .SingleInstance()
              .AutoActivate();
        }
    }
}
