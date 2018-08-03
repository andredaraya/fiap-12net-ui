using System.Threading.Tasks;

namespace GeekBurger.Ui.Domain.Interface
{
    public interface IStoreCatalogService
    {
        Task<bool> GetStoreCatalog();
        Task<bool> GetProducts();
    }
}
