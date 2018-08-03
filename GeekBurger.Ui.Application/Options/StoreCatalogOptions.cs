using Flurl;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace GeekBurger.Ui.Application.Options
{
    public class StoreCatalogOptions
    {
        private const string SECTION_NAME = "StoreCatalogService";

        public string EndPoint { get; set; }
        public string GetStoreCatalog { get; set; }
        public string GetProducts { get; set; }

        public StoreCatalogOptions(IConfiguration configuration)
        {
            var section = configuration.GetSection(SECTION_NAME);
            var endpoint = section["EndPoint"];
            var getStoreCatalog = section["GetStoreCatalog"];
            var getProducts = section["GetProducts"];

            ValidateConfiguration(endpoint, getStoreCatalog, getProducts);

            this.EndPoint = endpoint;
            this.GetStoreCatalog = Url.Combine(endpoint, getStoreCatalog);
            this.GetProducts = Url.Combine(endpoint, getProducts);
        }

        private void ValidateConfiguration(string endpoint, string getStoreCatalog, string getProducts)
        {
            if (string.IsNullOrEmpty(endpoint))
                throw new KeyNotFoundException($"Config key {SECTION_NAME}:Endpoint not set.");
            if (string.IsNullOrEmpty(getStoreCatalog))
                throw new KeyNotFoundException($"Config key {SECTION_NAME}:GetStoreCatalog not set.");
            if (string.IsNullOrEmpty(getProducts))
                throw new KeyNotFoundException($"Config key {SECTION_NAME}:GetProducts not set.");
        }
    }
}
