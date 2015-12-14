using System;
using System.Collections.Generic;
using System.Linq;
using VTP2015.DataAccess.UnitOfWork;
using VTP2015.Entities;

namespace UnitTests.Mock
{
    class MockUnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, IEnumerable<BaseEntity>> _results;

        public MockUnitOfWork()
        {
            _results = new Dictionary<Type, IEnumerable<BaseEntity>>();
        }

        public void AddResult<T>(IEnumerable<T> result) where T : BaseEntity
        {
            _results.Add(typeof(T), result);
        }

        public IRepository<T> Repository<T>() where T : BaseEntity
        {
            var result = _results.FirstOrDefault(results => results.Key.Name == typeof (T).Name).Value;

            return result != null ? new MockRepository<T>(result) : new MockRepository<T>();
        }
    }
}
