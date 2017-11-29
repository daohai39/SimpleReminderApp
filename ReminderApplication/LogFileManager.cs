using System;
using System.IO;

namespace ReminderApplication
{
    public class LogFileManager : IFileManager
    {
        private readonly string _path;

        public LogFileManager()
        {
            _path = @"E:\C# course\text_logger.txt";
        }

        public LogFileManager(string path)
        {
            if(!ValidPath(path))
                throw new ArgumentException("Invalid path");
            _path = path;
        }

        public void WriteFile(string message)
        {
            using (var streamWriter = new StreamWriter(_path, true))
            {
                streamWriter.WriteLine(message);
            }
        }

        public string ReadFile()
        {
            var result = "";
            using (var streamReader = new StreamReader(_path))
            {
                while (!streamReader.EndOfStream)
                    result += streamReader.ReadLine();
            }
            return result;
        }

        private bool ValidPath(string path) => (!string.IsNullOrWhiteSpace(path) &&
                                                 path.EndsWith(".txt", StringComparison.CurrentCultureIgnoreCase)); 
    }
}