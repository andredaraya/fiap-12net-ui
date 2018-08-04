using Flurl.Http;
using GeekBurger.Ui.Application.Options;
using GeekBurger.Ui.Domain.Interface;
using GeekBurger.Ui.Domain.Request;
using GeekBurger.Ui.Domain.Response;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GeekBurger.Ui.Application.ExternalServices
{
    public class StoreCatalogService : IStoreCatalogService
    {
        private readonly StoreCatalogOptions _options;

        public StoreCatalogService(StoreCatalogOptions options)
        {
            this._options = options;
        }

        public virtual async Task<Guid?> GetStoreCatalog(CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = new GetStoreCatalogResponse();
            try
            {
                var serviceResult = await _options.GetStoreCatalog.WithHeader("Cache-Control", "no-cache")
                                                 .SetQueryParam("")
                                                .GetAsync(cancellationToken);

                response = JsonConvert.DeserializeObject<GetStoreCatalogResponse>(serviceResult.Content.ReadAsStringAsync().Result);

                if (response.Ready)
                    return response.StoreId;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }

        public virtual async Task<GetProductsResponse> GetProducts(GetProductsRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = new GetProductsResponse();
            try
            {
                var serviceResult = await _options.GetProducts.WithHeader("Cache-Control", "no-cache")
                                                 .SetQueryParams(request)
                                                 .GetAsync(cancellationToken);

                response = JsonConvert.DeserializeObject<GetProductsResponse>(serviceResult.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}
