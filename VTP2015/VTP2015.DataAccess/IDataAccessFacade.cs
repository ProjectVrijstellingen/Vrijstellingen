using VTP2015.DataAccess.Bamaflex;
using VTP2015.DataAccess.Identity;

namespace VTP2015.DataAccess
{
    public interface IDataAccessFacade 
    {
        Context Context { get; set; }

        Facade Bamaflex { get; set; }

        IdentityManagementWebservice Identity { get; set; }

    }
}
