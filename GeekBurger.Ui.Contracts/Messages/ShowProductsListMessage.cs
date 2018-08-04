using System.Collections.Generic;

namespace GeekBurger.Ui.Contracts.Messages
{
    public class ShowProductsListMessage
    {
        public ShowProductsListMessage()
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
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public List<Item> Items { get; set; }
        public int Price { get; set; }
        public int RequesterId { get; set; }
    }

    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
    }

}
