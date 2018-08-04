using System.Collections.Generic;

namespace GeekBurger.Ui.Domain.Response
{
    public class GetProductsResponse
    {
        public GetProductsResponse()
        {
            this.Products = new List<Product>();
        }

        public List<Product> Products { get; set; }
    }

    public class Product
    {
        public Product()
        {
            this.Items = new List<Item>();
        }

        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public List<Item> Items { get; set; }
        public string Price { get; set; }
    }

    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
    }
}
