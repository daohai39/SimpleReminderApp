using System;
using System.Collections.Generic;
using System.IO;

namespace ReminderApplication
{
    public class DataFileManager : TextFileManager, IDataAccess<Reminder>
    {
        private readonly IStringConverter<Reminder> _converter;
        private string Path { get; } = @"E:\C# course\reminder.txt";

        public DataFileManager(IStringConverter<Reminder> converter)
            : base()
        {
            _converter = converter;
        }

        public Reminder[] LoadData()
        {
            var readData = GetDataFromFile();
            var reminders = new Reminder[readData.Length];
            for (var i = 0; i < readData.Length; i++)
                reminders[i] = _converter.ConvertToObject(readData[i]); 
            return reminders;
        }

        public bool SaveData(IList<Reminder> reminderList)
        {
            if (reminderList.Count == 0) throw new ArgumentException("List is empty", nameof(reminderList));
            var remindersInString = "";
            foreach (var reminder in reminderList)
            {
                remindersInString += _converter.ConvertToString(reminder) + "\r\n";
            }
            Copy(Path, @"E:\C# course\Backup.txt", true);
            WriteAllText(Path, remindersInString);
            return true;
        }

        private string[] GetDataFromFile()
        {
            var unpackedString = ReadFile(Path);  
            var readData = unpackedString.Replace("\r\n", "\n")
                .TrimEnd('\n')
                .Split('\n');
            return readData;
        }                

        private void WriteAllText(string path, string message)
        {
            if (!ValidPath(path)) throw new ArgumentException("Invalid Path");
            File.WriteAllText(path, message);
        }           

        private void Copy(string source, string destination, bool canOverride)
        {
            if (!ValidPath(source)) throw new ArgumentException("Invalid Path");
            if (!ValidPath(destination)) throw new ArgumentException("Invalid Copy Path");
            File.Copy(source, destination, canOverride);
        }
    }
}