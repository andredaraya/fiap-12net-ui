using System.Collections.Generic;

namespace GeekBurger.Ui.Contracts.Request
{
    public class CreateOrderRequest
    {
        public CreateOrderRequest()
        {
            this.Products = new List<Product>();
            this.ProductionIds = new List<int>();
        }


        public int OrderId { get; set; }
        public List<Product> Products { get; set; }
        public List<int> ProductionIds { get; set; }
    }

    public class Product
    {
        public int ProductId { get; set; }
        public string Price { get; set; }
    }

}
