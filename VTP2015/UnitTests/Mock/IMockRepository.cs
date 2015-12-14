using System.Linq;
using VTP2015.Entities;

namespace UnitTests.Mock
{
    internal interface IMockRepository
    {
        BaseEntity GetById(object id);
        void Insert(BaseEntity entity);
        void Update(BaseEntity entity);
        void Delete(BaseEntity entity);
        IQueryable<BaseEntity> Table { get; }
    }
}
