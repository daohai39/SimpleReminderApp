using System;
using System.IO;
using System.Runtime.CompilerServices;

[assembly:
    InternalsVisibleTo("ReminderApplication.UnitTests")]
namespace ReminderApplication
{
    public class TextFileManager : IFileManager
    {
        public void WriteAllText(string path, string message)
        {
            if (!ValidPath(path)) throw new ArgumentException("Invalid Path");
            File.WriteAllText(path, message);
        }

        public void WriteFile(string path, string message)
        {
            if (!ValidPath(path)) throw new ArgumentException("Invalid Path");
            using (var writer = new StreamWriter(path, true))
                WriteFile(message, writer);
        }

        internal void WriteFile(string message, TextWriter writer)
        {
            writer.WriteLine(message);
        }

        public void Copy(string source, string destination, bool canOverride)
        {
            if (!ValidPath(source)) throw new ArgumentException("Invalid Path");
            if (!ValidPath(destination)) throw new ArgumentException("Invalid Path");
            File.Copy(source, destination, canOverride);
        }

        public string ReadFile(string path)
        {
            if (!ValidPath(path)) throw new ArgumentException("Invalid Path");
            var result = "";
            using (var reader = new StreamReader(path))
                result += ReadFile(reader);
            return result;
        }

        internal string ReadFile(TextReader reader)
        {
            return reader.ReadToEnd();
        }

        private bool ValidPath(string path) => (!string.IsNullOrWhiteSpace(path) &&
                                                path.EndsWith(".txt", StringComparison.CurrentCultureIgnoreCase));
    }
}