using Flurl.Http;
using GeekBurger.Ui.Application.Options;
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

        public virtual async Task<bool> PostUser()
        {
            var response = false;
            try
            {
                CancellationTokenSource tokenSource = new CancellationTokenSource();
                tokenSource.CancelAfter(3000);

                var serviceResult = await _options.PostUser.WithHeader("Cache-Control", "no-cache")
                                                .PostJsonAsync("", tokenSource.Token);

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

        public virtual async Task<bool> PostFoodRestrictions()
        {
            var response = false;
            try
            {
                CancellationTokenSource tokenSource = new CancellationTokenSource();
                tokenSource.CancelAfter(3000);

                var serviceResult = await _options.PostFoodRestrictions.WithHeader("Cache-Control", "no-cache")
                                                .PostJsonAsync("", tokenSource.Token);

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
