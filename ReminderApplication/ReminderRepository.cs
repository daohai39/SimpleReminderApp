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
            Save();
        }

        public override void Delete(Reminder entity)
        {
            if (!ValidReminder(entity)) throw new ArgumentException("Invalid Argument", nameof(entity));
            _reminderList.Remove(entity);
//            Save();
        }

        public override List<Reminder> GetAll()
        {
            return _reminderList;
        }

        private void Save()
        {
            try
            {
                _connection.Connect();
                using (var streamWriter = _connection.Connection.AppendText())
                {
                    foreach (var reminder in _reminderList)
                    {
                        var reminderToString = _converter.ConvertToString(reminder);
                        streamWriter.WriteLineAsync(reminderToString);
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex);
            }
        }

        private bool ValidReminder(Reminder reminder) => true;
    }
}