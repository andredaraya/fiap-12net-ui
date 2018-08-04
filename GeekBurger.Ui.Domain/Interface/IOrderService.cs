using System.Threading;
using System.Threading.Tasks;

namespace GeekBurger.Ui.Domain.Interface
{
    public interface IOrderService
    {
        Task<bool> CreateOrder(CancellationToken cancellationToken = default(CancellationToken));
    }
}
