using System;

namespace GeekBurger.Ui.Domain.Response
{
    public class GetStoreCatalogResponse
    {
        public Guid StoreId { get; set; }
        public bool Ready { get; set; }
    }
}
