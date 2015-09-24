using System.Linq;
using VTP2015.Entities;

namespace VTP2015.Repositories.Interfaces
{
    public interface IBewijsRepository
    {
        bool IsBewijsFromStudent(string email);
        Evidence GetById(int bewijsId);
        IQueryable<Evidence> GetByStudent(string email);
        Evidence Insert(Evidence entity);
        string Delete(object id);
    }
}
