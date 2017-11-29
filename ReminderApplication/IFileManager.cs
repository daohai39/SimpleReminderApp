namespace ReminderApplication
{
    public interface IFileManager
    {
        void WriteFile(string message);
        string ReadFile();
    }
}