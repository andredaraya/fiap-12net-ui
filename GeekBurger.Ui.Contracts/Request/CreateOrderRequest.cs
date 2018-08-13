using GeekBurger.Ui.Contracts.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeekBurger.Ui.Contracts.Request
{
    public class CreateOrderRequest
    {
        public CreateOrderRequest()
        {
            this.ProductsOrder = new List<Product>();
            this.ProductionIds = new List<Guid>();
        }

        public Guid OrderId { get; set; }
        public List<Product> ProductsOrder { get; set; }
        public List<Guid> ProductionIds { get; set; }

        public NewOrderMessage ConvertToNewOrderMessage()
        {
            NewOrderMessage newOrder = new NewOrderMessage()
            {
                OrderId = OrderId,
                ProductionIds = ProductionIds,
                Products = ProductsOrder.Select(p => new ProductMessage() { ProductId = p.ProductId }).ToList(),
                Total = this.ProductsOrder.Sum(p => p.Price)
            };

            return newOrder;
        }
    }

    public class Product
    {
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
    }


}
