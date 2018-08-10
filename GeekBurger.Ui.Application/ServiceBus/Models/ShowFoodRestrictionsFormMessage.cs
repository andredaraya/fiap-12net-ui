using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Ui.Application.ServiceBus.Models
{
    public class ShowFoodRestrictionsFormMessage
    {
        public Guid UserId { get; set; }
        public Guid RequesterId { get; set; }
    }
}
