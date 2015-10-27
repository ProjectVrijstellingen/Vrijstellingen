using StructureMap.Configuration.DSL;
using VTP2015.DataAccess.UnitOfWork;
using VTP2015.ServiceLayer.Admin;
using VTP2015.ServiceLayer.Authentication;
using VTP2015.ServiceLayer.Counselor;
using VTP2015.ServiceLayer.Feedback;
using VTP2015.ServiceLayer.Lecturer;
using VTP2015.ServiceLayer.Student;

namespace VTP2015.Infrastructure.Registries
{
    public class MockServiceLayerRegistry : Registry
    {
        public MockServiceLayerRegistry()
        {
            Scan(scan =>
            {
                For<IUnitOfWork>().Use<UnitOfWork>();
                For<IAdminFacade>().Use<MockAdminFacade>();
                For<IAuthenticationFacade>().Use<MockAuthenticationFacade>();
                For<ICounselorFacade>().Use<MockCounselorFacade>();
                For<IFeedbackFacade>().Use<MockFeedbackFacade>();
                For<ILecturerFacade>().Use<MockLecturerFacade>();
                For<IStudentFacade>().Use<MockStudentFacade>();
            });

        }
    }
}