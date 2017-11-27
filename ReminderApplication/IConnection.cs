namespace ReminderApplication
{
    public interface IConnection<T> 
    {
        T Connection { get; }
        bool IsConnect { get; }
        bool Connect();
        bool Disconnect();
    }
}