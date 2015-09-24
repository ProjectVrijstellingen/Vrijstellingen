using StructureMap.Configuration.DSL;
using VTP2015.DataAccess;
using VTP2015.Repositories.Implementations;
using VTP2015.Repositories.Interfaces;
using VTP2015.Repositories.Remote_Services;

namespace VTP2015.Infrastructure.Registries
{
    public class RepostitoryRegistry : Registry
    {
        public RepostitoryRegistry()
        {
            Scan(scan =>
            {
                For<IDataAccessFacade>().Use<DataAccessFacade>();
                For<IAanvraagRepository>().Use<AanvraagRepository>();
                For<IStudentRepository>().Use<StudentRepository>();
                For<IDossierRepository>().Use<DossierRepository>();
                For<ILoginRepository>().Use<LoginRepository>();
                For<IBewijsRepository>().Use<BewijsRepository>();
                For<IOpleidingRepository>().Use<OpleidingRepository>();
                For<IDocentRepository>().Use<DocentRepository>();
                For<IBamaflexRepository>().Use<BamaflexRepository>();
                For<IIdentityRepository>().Use<IdentityRepository>();
                For<IPartimInformatieRepository>().Use<PartimInformatieRepository>();
                For<IFeedbackRepository>().Use<FeedbackRepository>();
            });
        }

    }
}