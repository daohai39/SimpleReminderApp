using System;
using System.IO;

namespace ReminderApplication
{
    public class TextConnection : IConnection<FileInfo>
    {
        private readonly string _path;
        public FileInfo Connection { get; private set; }
        public bool IsConnect { get; private set; }

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
            if (IsConnect) Disconnect();
            try
            {
                Connection = new FileInfo(_path);
                IsConnect = true;
                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid path", ex);
            } 
        }

        public bool Disconnect()
        {
            Connection = null;
            IsConnect = false;
            return true;
        }

        private bool ValidPath(string path) => !string.IsNullOrWhiteSpace(path);
    }
}