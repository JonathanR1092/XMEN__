using System.Collections.Generic;

namespace XMEN.Abstractions.Interfaces
{
    public interface ICrud<T>
    {
        T Save(T entity);

        IList<T> List();

        T ListById(int id);

        void Delete(int id);
    }
}