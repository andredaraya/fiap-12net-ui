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

        public virtual async Task<bool> CreateOrder()
        {
            var response = false;
            try
            {
                CancellationTokenSource tokenSource = new CancellationTokenSource();
                tokenSource.CancelAfter(3000);

                var serviceResult = await _options.CreateOrder.WithHeader("Cache-Control", "no-cache")
                                                .PostJsonAsync(tokenSource.Token);

               response = JsonConvert.DeserializeObject<bool>(serviceResult.Content.ReadAsStringAsync().Result);
            }
            catch (FlurlHttpException ex)
            {
                if (ex.InnerException is TaskCanceledException)
                {
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}
