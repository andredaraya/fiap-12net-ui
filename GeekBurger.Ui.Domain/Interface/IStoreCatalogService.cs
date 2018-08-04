using System.Threading;
using System.Threading.Tasks;

namespace GeekBurger.Ui.Domain.Interface
{
    public interface IStoreCatalogService
    {
        Task<bool> GetStoreCatalog(CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> GetProducts(CancellationToken cancellationToken = default(CancellationToken));
    }
}
