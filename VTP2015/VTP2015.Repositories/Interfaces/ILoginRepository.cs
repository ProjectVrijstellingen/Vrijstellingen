using System.Collections.Generic;
using VTP2015.DataAccess.Bamaflex;
using VTP2015.Entities;

namespace VTP2015.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        IEnumerable<Counselor> TrajectBegeleiders { get; }
        bool IsBegeleider(string email);
        void RemoveBegeleider(string email);
        void AddBegeleider(string email);
        string GetOpleiding(string email);
        void ChangeOpleiding(string email, Opleiding opleiding);
    }
}