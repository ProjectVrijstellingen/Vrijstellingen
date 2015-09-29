using System.Linq;
using VTP2015.ServiceLayer.Admin.Models;
namespace VTP2015.ServiceLayer.Admin
{
    public interface IAdminFacade
    {
        void InsertCounselor(string email);
        void RemoveCounselor(string email);

    }
}
