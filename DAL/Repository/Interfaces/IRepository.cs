using System.Collections.Generic;

namespace DAL.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Single(int id);
        void Add(T entity);
        void Edit(T entity);
        void Delete(T entity);
    }
}