namespace ReminderApplication
{
    public interface IStringConverter<T> where T : Reminder
    {
        string ConvertToString(T entity);
        T ConvertToObject(string entityString);
    }
}