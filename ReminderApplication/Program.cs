using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ReminderApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileManager = new TextFileManager();
            var logger = new TextLogger(fileManager);
            var converter = new ReminderToStringConverter();
            var dataFileManager = new DataFileManager(converter);
            var repository = new ReminderRepository();           
                           
            var reminderApp = new ReminderApp(repository, logger, dataFileManager);
            reminderApp.Run();                                

            var disposeReminder = new Reminder {Content = "Dispose reminder", CreatedAt = DateTime.Now};
            var oldReminder = new Reminder {Content = "Old reminder", CreatedAt = DateTime.Now};
            reminderApp.AddReminder(disposeReminder);
            reminderApp.AddReminder(oldReminder);
            reminderApp.DeleteReminder(disposeReminder);
            var newReminder = new Reminder {Content = "New reminder", CreatedAt = DateTime.Now};
            reminderApp.UpdateReminder(oldReminder, newReminder);
            foreach (var reminder in repository.GetAll())
                Console.WriteLine(reminder.ToString());
            reminderApp.Close();
            Thread.Sleep(10000);
        }
    }
}
