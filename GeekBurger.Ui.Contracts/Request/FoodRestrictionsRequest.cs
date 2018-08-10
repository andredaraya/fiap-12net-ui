using System;
using System.Collections.Generic;

namespace GeekBurger.Ui.Contracts.Request
{
    public class FoodRestrictionsRequest
    {
        public FoodRestrictionsRequest()
        {
            this.Restrictions = new List<string>();
        }

        public List<string> Restrictions { get; set; }
        public string Others { get; set; }
        public Guid UserId { get; set; }
        public Guid RequesterId { get; set; }
    }
}
