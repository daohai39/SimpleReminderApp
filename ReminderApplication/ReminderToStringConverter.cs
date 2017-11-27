using System;

namespace ReminderApplication
{
    public class ReminderToStringConverter : IStringConverter<Reminder>
    {
        public string ConvertToString(Reminder entity)
        {
            return entity.Content;
        }

        public Reminder ConvertToObject(string entityString)
        {
             return new Reminder {Content = entityString, CreatedAt = DateTime.Now};
        }
    }
}