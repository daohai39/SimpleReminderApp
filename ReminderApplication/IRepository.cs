
using System.Collections.Generic;

namespace ReminderApplication
{
    public interface IRepository<T> where T:Reminder
    {
//        public abstract T GetById(int id);
        T GetByName(string name);
        void Insert(T reminder);
        void Delete(T entity);
        List<T> GetAll();
        void Update(T oldEntity, T newEntity);
        bool Load(IList<T> entities);
    }
        
}