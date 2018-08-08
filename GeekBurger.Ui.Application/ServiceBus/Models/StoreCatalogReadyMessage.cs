using System;

namespace GeekBurger.Ui.Application.ServiceBus.Models
{
    public class StoreCatalogReadyMessage
    {
        public Guid StoreId { get; set; }
        public bool Ready { get; set; }
    }
}
