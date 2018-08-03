﻿using Flurl.Http;
using GeekBurger.Ui.Application.Options;
using GeekBurger.Ui.Domain.Interface;
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

        public virtual async Task<bool> GetStoreCatalog()
        {
            var response = false;
            try
            {
                CancellationTokenSource tokenSource = new CancellationTokenSource();
                tokenSource.CancelAfter(3000);

                var serviceResult = await _options.GetStoreCatalog.WithHeader("Cache-Control", "no-cache")
                                                 .SetQueryParam("")
                                                .GetAsync(tokenSource.Token);

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

        public virtual async Task<bool> GetProducts()
        {
            var response = false;
            try
            {
                CancellationTokenSource tokenSource = new CancellationTokenSource();
                tokenSource.CancelAfter(3000);

                var serviceResult = await _options.GetProducts.WithHeader("Cache-Control", "no-cache")
                                                 .SetQueryParam("")
                                                 .GetAsync(tokenSource.Token);

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