using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.DomainModel;
using DAL.DomainModel.Interfaces;
using DAL.Repository.Interfaces;

namespace DAL.Repository
{
    public class Repository: IRepository
    {
        private readonly EntityDbContext _context;

        public Repository(EntityDbContext context)
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
            if (entity is IAuditable)
            {
                _context.Entry(entity).Property("Created").CurrentValue = DateTime.Now;
                _context.Entry(entity).Property("Modified").CurrentValue = DateTime.Now;
            }
            if (entity is IDeletable)
            {
                _context.Entry(entity).Property("Deleted").CurrentValue = false;
            }
            _context.Set<TEntity>().Add(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity is IAuditable)
                _context.Entry(entity).Property("Modified").CurrentValue = DateTime.Now;
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

        public IQueryable Queryable(Type type)
        {
            return _context.Set(type);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}