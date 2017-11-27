namespace ReminderApplication
{
    public class SystemLogger : ILogger
    {
        public static void Log(string messageType, string error)
        {
            
        }

        public void LogError(string messageType, string error)
        {
            throw new System.NotImplementedException();
        }

        public void LogInfo(string messageType, string info)
        {
            throw new System.NotImplementedException();
        }
    }
}