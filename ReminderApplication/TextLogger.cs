using System.IO;

namespace ReminderApplication
{
    public class TextLogger : ILogger
    {
        private readonly string _path;

        public TextLogger()
        {
            _path = @"E:\C# course\text_logger.txt";
        }

        public TextLogger(string path)
        {
            _path = path;
        }

        public void LogError(string messageType, string error)
        {
            using (var streamWriter = new StreamWriter(_path, true))
            {
                streamWriter.WriteLine(messageType + ": " + error);
            }
        }

        public void LogInfo(string messageType, string info)
        {
            using (var streamWriter = new StreamWriter(_path, true))
            {
                streamWriter.WriteLine(messageType + ": " + info);
            }
        }
    }
}