using System;

namespace ReminderApplication
{
    public class TextLogger : ILogger
    {
        public IFileManager FileManager { get; }

        public TextLogger()
        {
            FileManager = new LogFileManager();
        }

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

        public string Format(string messageType, string message) => $"{messageType} : {message} : {DateTime.Now}";
    }
}