using System;
using System.IO;
using System.Runtime.CompilerServices;

[assembly:
    InternalsVisibleTo("ReminderApplication.UnitTests")]

namespace ReminderApplication
{
    public class ReminderApp
    {
        private readonly IRepository<Reminder> _reminderRepository;
        private readonly ILogger _logger;
        private readonly IDataAccess<Reminder> _dataAccessor;

        public bool IsRunning { get; internal set; }

        public ReminderApp(IRepository<Reminder> reminderRepository, ILogger logger, IDataAccess<Reminder> dataAccess)
        {
            _reminderRepository = reminderRepository;
            _logger = logger;
            _dataAccessor = dataAccess;
        }

        public void Run()
        {
            if (IsRunning) return;
            try
            {
                var reminders = _dataAccessor.LoadData();
                IsRunning = _reminderRepository.Load(reminders);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _logger.LogError($"{ex.Message}");
            }
        }

        public void Close()
        {
            if (!IsRunning) return;
            try
            {
                var reminderList = _reminderRepository.GetAll();
                _dataAccessor.SaveData(reminderList);
                IsRunning = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _logger.LogError($"{ex.Message}");
            }
        }

        public void AddReminder(Reminder reminder)
        {
            if (!IsRunning) return;
            try
            {
                _reminderRepository.Insert(reminder);
                _logger.LogInfo($"Added new reminder: {reminder}");  
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _logger.LogError($"{ex.Message}");
            }
        }

        public void DeleteReminder(string name)
        {
            throw new NotImplementedException();
            try
            {
                var reminder = _reminderRepository.GetByName(name);
                _reminderRepository.Delete(reminder);
                _logger.LogInfo($"Delete reminder: {reminder}"); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _logger.LogError($"{ex.Message}");
            }
        }

        public void DeleteReminder(Reminder reminder)
        {
            if (!IsRunning) return;
            try
            {                                                  
                _reminderRepository.Delete(reminder);
                _logger.LogInfo($"Deleted reminder: {reminder}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _logger.LogError($"{ex.Message}");
            }
        }

        public void UpdateReminder(Reminder oldReminder, Reminder newReminder)
        {
            if (!IsRunning) return;
            try
            {
                _reminderRepository.Update(oldReminder, newReminder);
                _logger.LogInfo($"Updated reminder: {oldReminder} replaced with {newReminder}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _logger.LogError($"{ex.Message}");
            }
        }
    }
}