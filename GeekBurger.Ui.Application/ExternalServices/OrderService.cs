using Flurl.Http;
using GeekBurger.Ui.Application.Options;
using GeekBurger.Ui.Domain.Interface;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GeekBurger.Ui.Application.ExternalServices
{
    public class OrderService : IOrderService
    {
        private readonly OrderOptions _options;

        public OrderService(OrderOptions options)
        {
            this._options = options;
        }

        public virtual async Task<bool> CreateOrder(CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = false;
            try
            {
                var serviceResult = await _options.CreateOrder.WithHeader("Cache-Control", "no-cache")
                                                .PostJsonAsync(cancellationToken);

               response = JsonConvert.DeserializeObject<bool>(serviceResult.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}
