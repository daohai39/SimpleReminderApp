using System.Collections.Generic;

namespace ReminderApplication
{
    public interface IDataAccess<T>
    {
        T[] LoadData();
        bool SaveData(IList<T> objects);
    }
}