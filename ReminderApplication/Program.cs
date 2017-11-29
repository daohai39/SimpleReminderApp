using System;
using System.Collections.Generic;

namespace ReminderApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileManager = new LogFileManager();
            var logger = new TextLogger(fileManager);
            var converter = new ReminderToStringConverter();
            var connection = new TextConnection(@"E:\C# course\reminder.txt");
            var repository = new ReminderRepository(connection, converter);           
                           
            var reminderApp = new ReminderApp(repository, logger);
            reminderApp.Run();                                

            var disposeReminder = new Reminder {Content = "Dispose reminder", CreatedAt = DateTime.Now};
            reminderApp.AddReminder(disposeReminder);
//            reminderApp.DeleteReminder(disposeReminder);
            var newReminder = new Reminder {Content = "New reminder", CreatedAt = DateTime.Now};
            reminderApp.UpdateReminder(disposeReminder, newReminder);
            foreach (var reminder in repository.GetAll())
                Console.WriteLine(reminder.ToString());
            reminderApp.Close();
        }
    }
}
