using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Ui.Application.ServiceBus.Models
{
    public class ShowFoodRestrictionsFormMessage
    {
        public int UserId { get; set; }
        public int RequesterId { get; set; }
    }
}
