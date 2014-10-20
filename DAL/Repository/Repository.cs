using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.DomainModel;
using DAL.Repository.Interfaces;

namespace DAL.Repository
{
    public class Repository: IRepository
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity: class
        {
            return _context.Set<TEntity>().ToList();
        }

        public TEntity Find<TEntity>(object id) where TEntity: class
        {
            return _context.Set<TEntity>().Find(id);
        }

        public void Add<TEntity>(TEntity entity) where TEntity: class
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete<TEntity>(TEntity entity) where TEntity: class
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Set<TEntity>().Attach(entity);
            }
            _context.Set<TEntity>().Remove(entity);
        }

        public void Delete<TEntity>(object id) where TEntity: class
        {
            var entity = _context.Set<TEntity>().Find(id);
            _context.Set<TEntity>().Remove(entity);
        }

        public IQueryable<TEntity> Queryable<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>();
        }
    }
}