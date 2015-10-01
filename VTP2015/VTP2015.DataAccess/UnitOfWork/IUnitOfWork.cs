using VTP2015.Entities;

namespace VTP2015.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        Repository<T> Repository<T>() where T : BaseEntity;
    }
}
