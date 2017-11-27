using System;
using System.IO;

namespace ReminderApplication
{
    public class TextConnection : IConnection<FileInfo>
    {
        private readonly string _path;
        public FileInfo Connection { get; private set; }

        public TextConnection(string path)
        {
            if(!ValidPath(path))
               throw new ArgumentNullException(nameof(path), "File name can not be empty");
            if (!File.Exists(path))
                File.Create(path);
            _path = path;
        }

        public bool Connect()
        {                 
            try
            {
                Connection = new FileInfo(_path);
                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid path");
            } 
        }

        public bool Disconnect()
        {
            return true;
        }

        private bool ValidPath(string path) => !String.IsNullOrWhiteSpace(path);
    }
}