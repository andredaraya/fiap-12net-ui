using System.Collections.Generic;

namespace GeekBurger.Ui.Domain.Request
{
    public class GetProductsRequest
    {
        public GetProductsRequest()
        {
            this.Restrictions = new List<string>();
        }
        public string StoreName { get; set; }
        public string UserId { get; set; }
        public List<string> Restrictions { get; set; }
    }
}
