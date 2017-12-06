using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: 
    InternalsVisibleTo("ReminderApplication.UnitTests")]

namespace ReminderApplication
{
    public class ReminderRepository : IRepository<Reminder>
    {
        internal List<Reminder> ReminderList { get; }


        public ReminderRepository()
        {                      
            ReminderList = new List<Reminder>();
        }


        public Reminder GetByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(Reminder reminder)
        {
            if(!ValidReminder(reminder)) throw new ArgumentException("Invalid reminder", nameof(reminder));
            ReminderList.Add(reminder);    
        }

        public void InsertAll(Reminder[] reminders)
        {   
            foreach (var reminder in reminders)
            {
                Insert(reminder);
            }
        }

        public void Delete(Reminder reminder)
        {
            if (ReminderList.Count == 0) throw new NullReferenceException("List is empty");
            if (!ValidReminder(reminder)) throw new ArgumentException("Invalid reminder", nameof(reminder));
            if (!ReminderList.Contains(reminder)) throw new ArgumentException("List does not have this reminder");
            ReminderList.Remove(reminder);
        }

        public List<Reminder> GetAll()
        {
            return new List<Reminder>(ReminderList);
        }

        public void Update(Reminder oldReminder, Reminder newReminder)
        {  
            if (ReminderList.Count == 0) throw new NullReferenceException("List is empty");
            if (!ValidReminder(newReminder)) throw new ArgumentException("Invalid Reminder");
            if (!ReminderList.Contains(oldReminder))
                throw new ArgumentException("List does not have this reminder");
            var index = ReminderList.IndexOf(oldReminder);
            ReminderList.RemoveAt(index);
            ReminderList.Insert(index, newReminder);
        }

        public bool Load(IList<Reminder> reminders)
        {
            if (reminders == null) return false;
            if (!reminders.All(ValidReminder)) return false;
            ReminderList.Clear();
            ReminderList.AddRange(reminders);
            return true;
        }

        private bool ValidReminder(Reminder reminder) => reminder.IsValid();
    }
}