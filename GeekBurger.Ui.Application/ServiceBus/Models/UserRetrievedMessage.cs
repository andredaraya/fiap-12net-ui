using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Ui.Application.ServiceBus.Models
{
    public class UserRetrievedMessage
    {
        public bool AreRestrictionsSet { get; set; }
        public int UserId { get; set; }
    }
}
