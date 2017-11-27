namespace ReminderApplication
{
    public interface ILogger
    {
        void LogError(string messageType, string error);
        void LogInfo(string messageType, string info);
    }
}