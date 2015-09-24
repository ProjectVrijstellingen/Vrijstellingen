using System.Linq;
using VTP2015.Entities;

namespace VTP2015.Repositories.Interfaces
{
    public interface IBewijsRepository
    {
        bool IsBewijsFromStudent(string email);
        Bewijs GetById(int bewijsId);
        IQueryable<Bewijs> GetByStudent(string email);
        Bewijs Insert(Bewijs entity);
        string Delete(object id);
    }
}
