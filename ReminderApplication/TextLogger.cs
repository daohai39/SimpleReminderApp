using System;

namespace ReminderApplication
{
    public class TextLogger : ILogger
    {
        private IFileManager FileManager { get; }

        public TextLogger(IFileManager fileManager)
        {
            FileManager = fileManager;
        } 

        public void LogError(string error)
        {
            FileManager.WriteFile(Format("Error", error));
        }

        public void LogInfo(string info)
        {
            FileManager.WriteFile(Format("Info", info));
        }

        private string Format(string messageType, string message) => $"{messageType} : {message} : {DateTime.Now}";
    }
}