using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Repository.Interfaces;

namespace DAL.Repository
{
    class Repository<TEntity> : IRepository<TEntity> where TEntity: class
    {

        private DbSet<TEntity> _dbSet;
        private DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public TEntity Find(object id)
        {
            return _dbSet.Find(id);
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
        }

        public IQueryable<TEntity> Queryable()
        {
            return _dbSet;
        }
    }
}