using System;

namespace GeekBurger.Ui.Contracts.Messages
{
    class ShowFoodRestrictionsFormMessage
    {
        public Guid UserId { get; set; }
        public Guid RequesterId { get; set; }
    }
}
