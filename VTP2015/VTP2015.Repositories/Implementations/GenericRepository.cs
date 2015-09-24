using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using VTP2015.DataAccess;
using VTP2015.Repositories.Interfaces;

namespace VTP2015.Repositories.Implementations
{
    public class GenericRepository<T>: IGenericRepository<T> where T : class
    {
        private readonly Context _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(Context context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<T> AsQueryable(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public virtual T Insert(T entity)
        {
            var result = _dbSet.Add(entity);
            SaveChanges();
            return result;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public virtual void Delete(object id)
        {
            var entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
            SaveChanges();
        }

        public virtual void Delete(T entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
            SaveChanges();
        }

        public virtual void Update(T entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
            SaveChanges();
        }

    }
}