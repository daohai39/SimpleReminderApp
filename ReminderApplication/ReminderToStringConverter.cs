﻿using System;

namespace ReminderApplication
{
    public class ReminderToStringConverter : IStringConverter<Reminder>
    {
        public string ConvertToString(Reminder entity)
        {
            if (entity == null) 
                throw new ArgumentException("Reminder can not be null");
            return $"{entity.Content.Trim()} - {entity.CreatedAt.ToString()}";
        }

        public Reminder ConvertToObject(string entityString)
        {
            if (string.IsNullOrWhiteSpace(entityString))
                throw new ArgumentException("Content can not be empty");
            var substrings = entityString.Split('-');
            if (substrings.Length != 2)
                throw new ArgumentException("Invalid string");
            var date = DateTime.MinValue;
            if (!DateTime.TryParse(substrings[1], out date))
                throw new ArgumentException("Invalid date format");
            var content = substrings[0].Trim();
            return new Reminder { Content = content, CreatedAt = date };
        }
    }
}