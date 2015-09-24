using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap.Configuration.DSL;
using VTP2015.ServiceLayer;
using VTP2015.ServiceLayer.Implementations;
using VTP2015.ServiceLayer.Interfaces;

namespace VTP2015.Infrastructure.Registries
{
    public class ServiceLayerRegistry : Registry
    {
        public ServiceLayerRegistry()
        {
            Scan(scan =>
            {
                For<IAuthenticationFacade>().Use<AuthenticationFacade>();
                For<ICounselorFacade>().Use<CounselorFacade>();
                For<IFeedbackFacade>().Use<FeedbackFacade>();
                For<ILecturerFacade>().Use<LecturerFacade>();
                For<IStudentFacade>().Use<StudentFacade>();
            });
        }
    }
}