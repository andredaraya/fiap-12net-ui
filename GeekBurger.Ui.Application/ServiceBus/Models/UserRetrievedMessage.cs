namespace GeekBurger.Ui.Application.ServiceBus.Models
{
    public class UserRetrievedMessage
    {
        public bool AreRestrictionsSet { get; set; }
        public int UserId { get; set; }
    }
}
