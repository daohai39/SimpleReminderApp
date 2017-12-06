using System.Collections.Generic;

namespace ReminderApplication
{
    public interface IDataAccess<T>
    {
        T[] LoadData();
        void SaveData(IList<T> objects);
    }
}