using System.Collections.Generic;

namespace GeekBurger.Ui.Contracts.Messages
{
    public class NewOrderMessage
    {
        public NewOrderMessage()
        {
            this.Products = new List<ProductMessage>();
            this.ProductionIds = new List<int>();
        }

        public int OrderId { get; set; }
        public string Total { get; set; }
        public List<ProductMessage> Products { get; set; }
        public List<int> ProductionIds { get; set; }
    }

    public class ProductMessage
    {
        public int ProductId { get; set; }
    }

}
