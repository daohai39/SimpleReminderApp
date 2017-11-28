using System;
using System.Collections.Generic;
using System.IO;

namespace ReminderApplication
{
    public class ReminderRepository : Repository<Reminder>
    {
        private readonly List<Reminder> _reminderList;
        private readonly IConnection<FileInfo> _connection;
        private readonly IStringConverter<Reminder> _converter;

        public ReminderRepository(IConnection<FileInfo> connection, IStringConverter<Reminder> converter)
        {
            _reminderList = new List<Reminder>();
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
            if(!ValidReminder(entity)) throw new ArgumentException("Invalid Argument", nameof(entity));
            _reminderList.Add(entity);    
        }

        public override void Delete(Reminder entity)
        {
            if (!ValidReminder(entity)) throw new ArgumentException("Invalid Argument", nameof(entity));
            _reminderList.Remove(entity);
        }

        public override List<Reminder> GetAll()
        {
            return _reminderList;
        }

        public override void Update(Reminder oldReminder, Reminder newReminder)
        {
            if (!_reminderList.Contains(oldReminder))
                throw new ArgumentException();
            var index = _reminderList.IndexOf(oldReminder);
            _reminderList.RemoveAt(index);
            _reminderList.Insert(index, newReminder);
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
                   foreach(var reminder in _reminderList)
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
            if (_reminderList.Count > 0) _reminderList.Clear();
            using (var streamReader = _connection.Connection.OpenText())
            {
                while (!streamReader.EndOfStream)
                    _reminderList.Add(_converter.ConvertToObject(streamReader.ReadLine()));
            }
        }

        private bool ValidReminder(Reminder reminder) => true;
    }
}