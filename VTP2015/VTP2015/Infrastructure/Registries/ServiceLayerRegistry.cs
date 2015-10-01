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
    public class ServiceLayerRegistry : Registry
    {
        public ServiceLayerRegistry()
        {
            Scan(scan =>
            {
                For<IUnitOfWork>().Use<UnitOfWork>();
                For<IAdminFacade>().Use<AdminFacade>();
                For<IAuthenticationFacade>().Use<AuthenticationFacade>();
                For<ICounselorFacade>().Use<CounselorFacade>();
                For<IFeedbackFacade>().Use<FeedbackFacade>();
                For<ILecturerFacade>().Use<LecturerFacade>();
                For<IStudentFacade>().Use<StudentFacade>();
            });
        }
    }
}