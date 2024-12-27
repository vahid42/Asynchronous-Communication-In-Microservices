namespace Order.API.Messaging
{
    public interface ISenderMessage
    {
        Task<bool> SendMessage(object message);
    }
}
