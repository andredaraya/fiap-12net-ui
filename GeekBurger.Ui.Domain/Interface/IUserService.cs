using GeekBurger.Ui.Contracts.Request;
using GeekBurger.Ui.Contracts.Response;
using System.Threading;
using System.Threading.Tasks;

namespace GeekBurger.Ui.Domain.Interface
{
    public interface IUserService
    {
        Task<bool> PostUser(byte[] face, CancellationToken cancellationToken = default(CancellationToken));
        Task<FoodRestrictionsResponse> PostFoodRestrictions(FoodRestrictionsRequest request, CancellationToken cancellationToken = default(CancellationToken));
    }
}
