using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repository.Interfaces
{
    public interface IRepository
    {
        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class;
        TEntity Find<TEntity>(object id) where TEntity : class;
        void Add<TEntity>(TEntity entity) where TEntity : class;
        void Update<TEntity>(TEntity entity) where TEntity : class;
        void Delete<TEntity>(TEntity entity) where TEntity : class;
        void Delete<TEntity>(object id) where TEntity : class;
        IQueryable<TEntity> Queryable<TEntity>() where TEntity : class;
        //IQueryable Queryable(Type type);
        void SaveChanges();
    }
}