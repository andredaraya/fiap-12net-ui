using System;
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

        public Guid StoreId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public List<Item> Items { get; set; }
        public decimal Price { get; set; }
        public Guid RequesterId { get; set; }
    }

    public class Item
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }
    }

}
