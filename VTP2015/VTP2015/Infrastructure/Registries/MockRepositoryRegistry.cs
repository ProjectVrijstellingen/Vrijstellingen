using StructureMap.Configuration.DSL;
using VTP2015.DataAccess.ServiceRepositories;

namespace VTP2015.Infrastructure.Registries
{
    public class MockRepositoryRegistry : Registry
    {
        public MockRepositoryRegistry()
        {
            Scan(scan =>
            {
                For<IBamaflexRepository>().Use<MockBamaflexRepository>();
                For<IIdentityRepository>().Use<MockIdentityRepository>();
            });

        }
    }
}