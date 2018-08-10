using System;
using System.Collections.Generic;

namespace GeekBurger.Ui.Contracts.Request
{
    public class CreateOrderRequest
    {
        public CreateOrderRequest()
        {
            this.Products = new List<Product>();
            this.ProductionIds = new List<Guid>();
        }


        public Guid OrderId { get; set; }
        public List<Product> Products { get; set; }
        public List<Guid> ProductionIds { get; set; }
    }

    public class Product
    {
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
    }

}
