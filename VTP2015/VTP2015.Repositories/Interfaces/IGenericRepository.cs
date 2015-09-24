using System;
using System.Linq;
using System.Linq.Expressions;

namespace VTP2015.Repositories.Interfaces
{
    interface IGenericRepository<T> where T: class
    {
        IQueryable<T> GetAll();
        IQueryable<T> AsQueryable(Expression<Func<T, bool>> predicate);
        T Insert(T entity);
        void SaveChanges();
        void Delete(object id);
        void Delete(T entityToDelete);
        void Update(T entityToUpdate);
    }
}
