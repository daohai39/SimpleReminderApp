using System;
using System.Collections.Generic;
using System.IO;

namespace ReminderApplication
{
    public class ReminderApp
    {
        private readonly IRepository<Reminder> _reminderRepository;
        private readonly ILogger _logger;
        private readonly IStringConverter<Reminder> _converter;

        public ReminderApp(IRepository<Reminder> reminderRepository, ILogger logger, IStringConverter<Reminder> converter)
        {
            this._reminderRepository = reminderRepository;
            this._logger = logger;
            this._converter = converter;
        }

        public void Run()
        {
            //Try load list of reminder from file       
            //Show load                                     
        }

        public void AddReminder(Reminder reminder)
        {
            try
            {
                _reminderRepository.Insert(reminder);
                _logger.LogInfo("Info", $"Added new reminder: {reminder.ToString()} - Date: {DateTime.Now}");  
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Invalid Reminder");
                _logger.LogError("Error", $"{ex.Message} - Date: {DateTime.Now}");
            }
        }

        public void DeleteReminder(int id)
        {
            try
            {
                var reminder = _reminderRepository.GetById(id);
                _reminderRepository.Delete(reminder);
                _logger.LogInfo("Info", $"Delete reminder: {reminder.ToString()} -  Date: {DateTime.Now}"); 
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Invalid Reminder");
                _logger.LogError("Error", $"{ex.Message} - Date: {DateTime.Now}");
            }
        }
        public void DeleteReminder(Reminder reminder)
        {
            try
            {                                                  
                _reminderRepository.Delete(reminder);
                _logger.LogInfo("Info", $"Delete reminder: {reminder.ToString()} -  Date: {DateTime.Now}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Invalid Reminder");
                _logger.LogError("Error", $"{ex.Message} - Date: {DateTime.Now}");
            }
        }

        public void UpdateReminder()
        {

        }

        private void Save()
        {
            
        }
        
    }
}