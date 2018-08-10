using System;
using System.Collections.Generic;
using messageProducts = GeekBurger.Ui.Contracts.Messages.Product;

namespace GeekBurger.Ui.Domain.Response
{
    public class GetProductsResponse
    {
        public GetProductsResponse()
        {
            this.Products = new List<Product>();
        }

        public List<Product> Products { get; set; }

        public List<messageProducts> ConvertToMessageProduct(List<Product> itens, Guid requesterId)
        {
            List<messageProducts> result = new List<messageProducts>();

            foreach (var product in itens)
            {
                messageProducts productMessage = new messageProducts();
                productMessage.Image = product.Image;
                productMessage.Name = product.Name;
                productMessage.Price = product.Price;
                productMessage.ProductId = product.ProductId;
                productMessage.StoreId = product.StoreId;
                productMessage.RequesterId = requesterId;

                foreach (var item in product.Items)
                {
                    productMessage.Items.Add(new Contracts.Messages.Item() { Name = item.Name, ItemId = item.ItemId });
                }
            }

            return result;
        }
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
    }

    public class Item
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }
    }
}
