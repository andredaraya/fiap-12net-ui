using System.Threading.Tasks;

namespace GeekBurger.Ui.Domain.Interface
{
    public interface IOrderService
    {
        Task<bool> CreateOrder();
    }
}
