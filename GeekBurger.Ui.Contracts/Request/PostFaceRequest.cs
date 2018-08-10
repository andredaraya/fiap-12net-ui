using System;

namespace GeekBurger.Ui.Contracts.Request
{
    public class PostFaceRequest
    {
        public byte[] Face { get; set; }
        public Guid RequesterId { get; set; }
    }
}
