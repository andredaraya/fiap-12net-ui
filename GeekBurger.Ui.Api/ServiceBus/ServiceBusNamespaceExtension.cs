using GeekBurger.Ui.Application.Options;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ServiceBus.Fluent;

namespace GeekBurger.Ui.Api.ServiceBus
{
    public static class ServiceBusNamespaceExtension
    {
        public static IServiceBusNamespace GetServiceBusNamespace(ServiceBusOptions configuration)
        {
            var credentials = SdkContext.AzureCredentialsFactory
                .FromServicePrincipal(configuration.ClientId, configuration.ClientSecret,
                        configuration.TenantId, AzureEnvironment.AzureGlobalCloud);

            var serviceBusManager = ServiceBusManager.Authenticate(credentials, configuration.SubscriptionId);
            return serviceBusManager.Namespaces.GetByResourceGroup(configuration.ResourceGroup, configuration.NamespaceName);
        }
    }
}
