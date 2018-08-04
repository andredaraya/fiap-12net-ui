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
        public int UserId { get; set; }
        public int RequesterId { get; set; }
    }
}
