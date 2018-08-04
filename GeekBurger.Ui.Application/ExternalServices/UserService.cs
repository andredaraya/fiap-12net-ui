using Flurl.Http;
using GeekBurger.Ui.Application.Options;
using GeekBurger.Ui.Contracts.Request;
using GeekBurger.Ui.Contracts.Response;
using GeekBurger.Ui.Domain.Interface;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GeekBurger.Ui.Application.ExternalServices
{
    public class UserService : IUserService
    {
        private readonly UserOptions _options;

        public UserService(UserOptions options)
        {
            this._options = options;
        }

        public virtual async Task<bool> PostUser(CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = false;
            try
            {
                var serviceResult = await _options.PostUser.WithHeader("Cache-Control", "no-cache")
                                                .PostJsonAsync("", cancellationToken);

                response = JsonConvert.DeserializeObject<bool>(serviceResult.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }

        public virtual async Task<FoodRestrictionsResponse> PostFoodRestrictions(FoodRestrictionsRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = new FoodRestrictionsResponse();
            try
            {
                var serviceResult = await _options.PostFoodRestrictions
                                                    .WithHeader("Cache-Control", "no-cache")
                                                    .PostJsonAsync(request, cancellationToken);

                response = JsonConvert.DeserializeObject<FoodRestrictionsResponse>(serviceResult.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}
