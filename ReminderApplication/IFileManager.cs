namespace ReminderApplication
{
    public interface IFileManager
    {
        void WriteFile(string path, string message);
//        void WriteAllText(string path, string message);
        string ReadFile(string path);
//        void Copy(string source, string destination, bool canOverride);
    }
}