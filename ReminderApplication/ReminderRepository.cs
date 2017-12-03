using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ReminderApplication
{
    public class ReminderRepository : Repository<Reminder>
    {
        public List<Reminder> ReminderList { get; private set; }
        private readonly IConnection<FileInfo> _connection;
        private readonly IStringConverter<Reminder> _converter;

#if DEBUG
        public ReminderRepository()
        {                      
            ReminderList = new List<Reminder>();
            _connection = null;
            _converter = null;
        }
#endif

        public ReminderRepository(IConnection<FileInfo> connection, IStringConverter<Reminder> converter)
        {
            ReminderList = new List<Reminder>();
            _connection = connection;
            _converter = converter;
        }
//
//        public Reminder GetById(int id)
//        {
//            throw new System.NotImplementedException();
//        }

        public override Reminder GetByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public override void Insert(Reminder entity)
        {
            if(!ValidReminder(entity)) throw new ArgumentException("Invalid reminder", nameof(entity));
            ReminderList.Add(entity);    
        }

        public override void Delete(Reminder entity)
        {
            if (!ValidReminder(entity)) throw new ArgumentException("Invalid reminder", nameof(entity));
            if (!ReminderList.Contains(entity)) throw new ArgumentException("List does not have this reminder");
            ReminderList.Remove(entity);
        }

        public override IList<Reminder> GetAll()
        {
            return ReminderList;
        }

        public override void Update(Reminder oldReminder, Reminder newReminder)
        {
            if (!ValidReminder(newReminder)) throw new ArgumentException("Invalid Reminder");
            if (!ReminderList.Contains(oldReminder))
                throw new ArgumentException("List does not have this reminder");
            var index = ReminderList.IndexOf(oldReminder);
            ReminderList.RemoveAt(index);
            ReminderList.Insert(index, newReminder);
        }

        public override void Save()
        {
            try
            {
                // Connect to original file
                if (!_connection.IsConnect)
                     _connection.Connect();
                //Copy List to another file
                var copyFile = @"E:\C# course\Reminder_copy.txt";
                using (var streamWriter = File.AppendText(copyFile))
                {
                   foreach(var reminder in ReminderList)
                   {
                       var reminderToString = _converter.ConvertToString(reminder);
                       streamWriter.WriteLineAsync(reminderToString);
                   }
                }
                //Replace original file with new file content
                var copy = new FileInfo(copyFile);
                copy.Replace(_connection.Connection.FullName, @"E:\C# course\Backup.txt");
                //Delete new file               
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex);
            }
        }

        public override void Load()
        {
            _connection.Connect();
            if (ReminderList.Count > 0) ReminderList.Clear();
            using (var streamReader = _connection.Connection.OpenText())
            {
                while (!streamReader.EndOfStream)
                    ReminderList.Add(_converter.ConvertToObject(streamReader.ReadLine()));
            }
        }

        private bool ValidReminder(Reminder reminder) => reminder.IsValid();
    }
}