using VTP2015.DataAccess.Bamaflex;
using VTP2015.DataAccess.Identity;

namespace VTP2015.DataAccess
{
    public class DataAccessFacade : IDataAccessFacade
    {
        public DataAccessFacade()
        {
            Context = new Context();
            Bamaflex = new Facade();
            Identity = new IdentityManagementWebservice();
        }

        public Context Context { get;  set; }

        public Facade Bamaflex { get;  set; }

        public IdentityManagementWebservice Identity { get;  set; }
    }
}
