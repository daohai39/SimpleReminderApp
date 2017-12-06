using System;
using System.Collections.Generic;
using System.IO;

namespace ReminderApplication
{
    public class DataFileManager : IDataAccess<Reminder>
    {
        private readonly IFileManager _fileManager;
        private readonly IStringConverter<Reminder> _converter;
        private string Path { get; } = @"E:\C# course\reminder.txt";

        public DataFileManager(IStringConverter<Reminder> converter,  IFileManager fileManager)
        {
            _fileManager = fileManager;
            _converter = converter;
        }

        public Reminder[] LoadData()
        {
            var unpackedString = _fileManager.ReadFile(Path);
            var readData = NormalizeData(unpackedString);
            var reminders = new Reminder[readData.Length];
            for (var i = 0; i < readData.Length; i++)
                reminders[i] = UnformatData(readData[i]);
            return reminders;
        }

        public void SaveData(IList<Reminder> reminderList)
        {
            var remindersInString = "";
            foreach (var reminder in reminderList)
            {
                remindersInString += FormatData(reminder);
            }
            _fileManager.Copy(Path, @"E:\C# course\Backup.txt", true);
            _fileManager.WriteAllText(Path, remindersInString);   
        }

        private Reminder UnformatData(string format)
        {
            return _converter.ConvertToObject(format);
        }

        private string FormatData(Reminder reminder)
        {
            return _converter.ConvertToString(reminder) + "\r\n";
        }

        private string[] NormalizeData(string unformattedString) => unformattedString.Replace("\r", "")
                                                                    .Split('\n');
    }
}