using System.Threading.Tasks;

namespace GeekBurger.Ui.Domain.Interface
{
    public interface IUserService
    {
        Task<bool> PostUser();
        Task<bool> PostFoodRestrictions();
    }
}
