namespace ReminderApplication
{
    public interface IConnection<T> 
    {
        T Connection { get; }
        bool Connect();
        bool Disconnect();
    }
}