
using System.Collections.Generic;

namespace ReminderApplication
{
    public interface IRepository<T> where T:Reminder
    {
        T GetById(int id);
        T GetByName(string name);
        void Insert(T entity);
        void Delete(T entity);
        List<T> GetAll();
    }
        
}