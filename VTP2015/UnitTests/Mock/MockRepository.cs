using System.Collections.Generic;
using System.Linq;
using VTP2015.DataAccess.UnitOfWork;
using VTP2015.Entities;

namespace UnitTests.Mock
{
    class MockRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IEnumerable<T> _result;

        public MockRepository()
        {
        }

        public MockRepository(IEnumerable<BaseEntity> result)
        {
            _result = result.Cast<T>();
        }

        public T GetById(object id)
        {
            return _result.First(result => result.Id == (int) id);
        }

        public void Insert(T entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable Table { get; }

        IQueryable<T> IRepository<T>.Table => _result.AsQueryable();
    }
}
