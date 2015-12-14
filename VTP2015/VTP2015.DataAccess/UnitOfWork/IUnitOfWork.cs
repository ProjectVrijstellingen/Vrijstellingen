using VTP2015.Entities;

namespace VTP2015.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : BaseEntity;
    }
}
