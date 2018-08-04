using GeekBurger.Ui.Domain.Request;
using GeekBurger.Ui.Domain.Response;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GeekBurger.Ui.Domain.Interface
{
    public interface IStoreCatalogService
    {
        Task<Guid?> GetStoreCatalog(CancellationToken cancellationToken = default(CancellationToken));
        Task<GetProductsResponse> GetProducts(GetProductsRequest request, CancellationToken cancellationToken = default(CancellationToken));
    }
}
