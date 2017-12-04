using System;

namespace ReminderApplication
{
    public class ReminderApp
    {
        private readonly Repository<Reminder> _reminderRepository;
        private readonly ILogger _logger;
        private bool IsRunning { get; set; }

        public ReminderApp(Repository<Reminder> reminderRepository, ILogger logger)
        {
            _reminderRepository = reminderRepository;
            _logger = logger;   
        }

        public void Run()
        {
            if (IsRunning) return;
            Load();
            IsRunning = true;
        }

        public void Close()
        {
            if (!IsRunning) return;
            Save();
            IsRunning = false;
        }

        public void AddReminder(Reminder reminder)
        {
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
            try
            {                                                  
                _reminderRepository.Delete(reminder);
                _logger.LogInfo($"Delete reminder: {reminder}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _logger.LogError($"{ex.Message}");
            }
        }

        public void UpdateReminder(Reminder oldReminder, Reminder newReminder)
        {
            try
            {
                _reminderRepository.Update(oldReminder, newReminder);
                _logger.LogInfo($"Update reminder: {oldReminder} replaced with {newReminder}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _logger.LogError($"{ex.Message}");
            }
        }

        private void Load()
        {
            _reminderRepository.Load();
        }

        private void Save()
        {
            _reminderRepository.Save();
        }

    }
}