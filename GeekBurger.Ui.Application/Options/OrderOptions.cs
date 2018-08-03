using Flurl;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace GeekBurger.Ui.Application.Options
{
    public class OrderOptions
    {
        private const string SECTION_NAME = "OrderService";

        public string EndPoint { get; set; }
        public string CreateOrder { get; set; }

        public OrderOptions(IConfiguration configuration)
        {
            var section = configuration.GetSection(SECTION_NAME);
            var endpoint = section["EndPoint"];
            var createOrder = section["CreateOrder"];

            ValidateConfiguration(endpoint, createOrder);

            this.EndPoint = endpoint;
            this.CreateOrder = Url.Combine(endpoint, createOrder);
        }

        private void ValidateConfiguration(string endpoint, string createOrder)
        {
            if (string.IsNullOrEmpty(endpoint))
                throw new KeyNotFoundException($"Config key {SECTION_NAME}:Endpoint not set.");
            if (string.IsNullOrEmpty(createOrder))
                throw new KeyNotFoundException($"Config key {SECTION_NAME}:CreateOrder not set.");
        }
    }
}
