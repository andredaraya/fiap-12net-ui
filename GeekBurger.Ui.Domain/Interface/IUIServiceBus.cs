namespace GeekBurger.Ui.Domain.Interface
{
    public interface IUIServiceBus
    {
        void AddToMessageList<T>(T messageObject, string label);
        void SendMessagesAsync();
    }
}
