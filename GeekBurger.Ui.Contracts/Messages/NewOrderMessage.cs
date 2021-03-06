﻿using System;
using System.Collections.Generic;

namespace GeekBurger.Ui.Contracts.Messages
{
    public class NewOrderMessage
    {
        public NewOrderMessage()
        {
            this.Products = new List<ProductMessage>();
            this.ProductionIds = new List<Guid>();
        }

        public Guid OrderId { get; set; }
        public decimal Total { get; set; }
        public List<ProductMessage> Products { get; set; }
        public List<Guid> ProductionIds { get; set; }
    }

    public class ProductMessage
    {
        public Guid ProductId { get; set; }
    }

}
