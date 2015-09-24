using System.Linq;
using VTP2015.Entities;

namespace VTP2015.Repositories.Interfaces
{
    public interface IOpleidingRepository
    {
        IQueryable<Opleiding> GetOpleidingen();
        Opleiding GetByEmail(string email);
    }
}
