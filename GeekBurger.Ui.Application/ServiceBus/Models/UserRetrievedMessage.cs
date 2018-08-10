using System;

namespace GeekBurger.Ui.Application.ServiceBus.Models
{
    public class UserRetrievedMessage
    {
        public bool AreRestrictionsSet { get; set; }
        public Guid UserId { get; set; }
    }
}
