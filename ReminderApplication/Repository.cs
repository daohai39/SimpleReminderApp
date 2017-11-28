
using System.Collections.Generic;

namespace ReminderApplication
{
    public abstract class Repository<T> where T:Reminder
    {
//        public abstract T GetById(int id);
        public abstract void Load();
        public abstract T GetByName(string name);
        public abstract void Insert(T entity);
        public abstract void Delete(T entity);
        public abstract List<T> GetAll();
        public abstract void Update(T oldEntity, T newEntity);
        public abstract void Save();
    }
        
}