using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Ui.Application.ServiceBus.Models
{
    public class StoreCatalogReadyMessage
    {
        public int StoreId { get; set; }
        public bool Ready { get; set; }
    }
}
