using System;

namespace ReminderApplication
{
    public class TextLogger : ILogger
    {
        public string Path { get; } = @"E:\C# course\text_logger.txt";
        private IFileManager FileManager { get; }

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
            FileManager.WriteFile(Path, Format("Error", error));
        }

        public void LogInfo(string info)
        {
            FileManager.WriteFile(Path, Format("Info", info));
        }

        public string Format(string messageType, string message) => $"{messageType} : {message} : {DateTime.Now}";
    }
}