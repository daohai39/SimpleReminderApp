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
            if (!ValidPath(path))
                throw new ArgumentException("Invalid path");
            _path = path;
        }

        public void WriteFile(string message)
        {
            using (var streamWriter = new StreamWriter(_path, true))
                WriteFile(message, streamWriter);
        }

        public void WriteFile(string message, TextWriter writer)
        {
            writer.WriteLine(message);
        }

        public string ReadFile()
        {
            using (var streamReader = new StreamReader(_path))
                return ReadFile(streamReader);
        }

        public string ReadFile(TextReader reader)
        {
            return reader.ReadToEnd();
        }

        private bool ValidPath(string path) => (!string.IsNullOrWhiteSpace(path) &&
                                                path.EndsWith(".txt", StringComparison.CurrentCultureIgnoreCase));
    }
}