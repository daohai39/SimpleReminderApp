using System;

namespace ReminderApplication
{
    public class ToStringRender : IStringConverter<Reminder>
    {
        public string ConvertToString(Reminder entity)
        {
            return entity.Content.ToString();
        }

        public Reminder ConvertToObject(string entityString)
        {
             return new Reminder {Content = entityString, CreatedAt = DateTime.Now};
        }
    }
}