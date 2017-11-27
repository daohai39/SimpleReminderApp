using System;
using System.Collections.Generic;

namespace ReminderApplication
{
    public class ReminderRepository : IRepository<Reminder>
    {
        private readonly List<Reminder> _reminderList;

        public ReminderRepository()
        {
            _reminderList = new List<Reminder>();
        }

        public Reminder GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Reminder GetByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(Reminder entity)
        {
            if(!ValidReminder(entity)) throw new ArgumentException("Invalid Argument", nameof(entity));
            _reminderList.Add(entity);
        }

        public void Delete(Reminder entity)
        {
            if (!ValidReminder(entity)) throw new ArgumentException("Invalid Argument", nameof(entity));
            _reminderList.Remove(entity);
        }

        public List<Reminder> GetAll()
        {
            return _reminderList;
        }

        private bool ValidReminder(Reminder reminder)
        {
            return true;
        }
    }
}