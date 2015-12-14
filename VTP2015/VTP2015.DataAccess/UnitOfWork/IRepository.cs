using System.Linq;
using VTP2015.Entities;

namespace VTP2015.DataAccess.UnitOfWork
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        IQueryable<T> Table { get; }
    }
}
