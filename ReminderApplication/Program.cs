using System;
using System.Collections.Generic;

namespace ReminderApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = new TextLogger();
            var converter = new ReminderToStringConverter();
            var connection = new TextConnection(@"E:\C# course\reminder.txt");
            var repository = new ReminderRepository(connection, converter);       
            var reminderList = new List<Reminder>
            {
                new Reminder {Content = "reminder 1", CreatedAt = DateTime.Now},
                new Reminder {Content = "reminder 2", CreatedAt = DateTime.Now},
                new Reminder {Content = "reminder 3", CreatedAt = DateTime.Now},
                new Reminder {Content = "reminder 4", CreatedAt = DateTime.Now},
                new Reminder {Content = "reminder 5", CreatedAt = DateTime.Now},
            };
                           
            var reminderApp = new ReminderApp(repository, logger);
            foreach (var reminder in reminderList)
                reminderApp.AddReminder(reminder);

            var disposeReminder = new Reminder {Content = "Dispose reminder", CreatedAt = DateTime.Now};
            reminderApp.AddReminder(disposeReminder);
            reminderApp.DeleteReminder(disposeReminder);
            foreach (var reminder in repository.GetAll())
                Console.WriteLine(reminder.ToString());
        }
    }
}
