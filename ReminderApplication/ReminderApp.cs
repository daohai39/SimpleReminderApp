using System;

namespace ReminderApplication
{
    public class ReminderApp
    {
        private readonly Repository<Reminder> _reminderRepository;
        private readonly ILogger _logger;

        public ReminderApp(Repository<Reminder> reminderRepository, ILogger logger)
        {
            _reminderRepository = reminderRepository;
            _logger = logger;   
        }

        public void Run()
        {
            _reminderRepository.Load();
        }

        public void AddReminder(Reminder reminder)
        {
            try
            {
                _reminderRepository.Insert(reminder);
                _logger.LogInfo($"Added new reminder: {reminder}");  
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Invalid Reminder");
                _logger.LogError($"{ex.Message}");
            }
        }

        public void DeleteReminder(string name)
        {
            try
            {
                var reminder = _reminderRepository.GetByName(name);
                _reminderRepository.Delete(reminder);
                _logger.LogInfo($"Delete reminder: {reminder}"); 
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Invalid Reminder");
                _logger.LogError($"{ex.Message}");
            }
        }
        public void DeleteReminder(Reminder reminder)
        {
            try
            {                                                  
                _reminderRepository.Delete(reminder);
                _logger.LogInfo($"Delete reminder: {reminder}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Invalid Reminder");
                _logger.LogError($"{ex.Message}");
            }
        }

        public void UpdateReminder(Reminder oldReminder, Reminder newReminder)
        {        
            _reminderRepository.Update(oldReminder, newReminder);
        }

        private void Save()
        {
            _reminderRepository.Save();
        }

        public int Close()
        {
            Save();
            return -1;
        }

    }
}