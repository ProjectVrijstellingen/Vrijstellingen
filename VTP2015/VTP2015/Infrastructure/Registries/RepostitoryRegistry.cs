using StructureMap.Configuration.DSL;
using VTP2015.DataAccess.ServiceRepositories;

namespace VTP2015.Infrastructure.Registries
{
    public class RepostitoryRegistry : Registry
    {
        public RepostitoryRegistry()
        {
            Scan(scan =>
            {
                For<IBamaflexRepository>().Use<BamaflexRepository>();
                For<IIdentityRepository>().Use<IdentityRepository>();
            });
        }

    }
}