using System;

namespace GeekBurger.Ui.Contracts.Response
{
    public class FoodRestrictionsResponse
    {
        public bool Processing { get; set; }
        public Guid UserId { get; set; }
    }
}
