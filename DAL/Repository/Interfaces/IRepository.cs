using System.Collections.Generic;
using System.Linq;

namespace DAL.Repository.Interfaces
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Find(object id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(object id);
        IQueryable<TEntity> Queryable();
    }
}