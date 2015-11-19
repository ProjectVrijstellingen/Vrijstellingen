using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTP2015.DataAccess.UnitOfWork;
using VTP2015.ServiceLayer.Lecturer.Models;

namespace VTP2015.ServiceLayer.Lecturer
{
    public class MockLecturerFacade : ILecturerFacade
    {
        private readonly IQueryable<RequestPartimInformation> mockdata;
        public MockLecturerFacade()
        {
            var req = new List<RequestPartimInformation> {
                new RequestPartimInformation
                {
                    Id = 1,
                    Module = new Module { Code="1", Name="Module" },
                    Partim = new Partim{ Code="1", Name="Partim" },
                    Argumentation = "Test 1",
                    File = new File{ },
                    Evidence = new List<Evidence> { new Evidence { Description="Evidence1", Path="Evidence.png", StudentEmail= "test@student.howest.be" } }.AsEnumerable(),
                    Student = new Lecturer.Models.Student { Id="1", Name="Gebruiker", Prename="Test", StudentMail = "test@student.howest.be" },
                    Status = Status.Untreated
                },
                new RequestPartimInformation{
                    Id = 2,
                    Module = new Module { Code="2", Name="Module2" },
                    Partim = new Partim{ Code="2", Name="Partim2" },
                    Argumentation = "Test 2",
                    File = new File{ },
                    Evidence = new List<Evidence> { new Evidence { Description="Evidence2", Path="Evidence.png", StudentEmail = "student@student.howest.be" } }.AsEnumerable(),
                    Student = new Lecturer.Models.Student { Id="2", Name="User", Prename="Test", StudentMail = "student@student.howest.be" },
                    Status = Status.Untreated
                },
                new RequestPartimInformation{
                    Id = 3,
                    Module = new Module { Code="3", Name="Module3" },
                    Partim = new Partim{ Code="3", Name="Partim3" },
                    Argumentation = "Test 3",
                    File = new File{ },
                    Evidence = new List<Evidence> { new Evidence { Description="Evidence3", Path="Evidence.png", StudentEmail = "student@student.howest.be" } }.AsEnumerable(),
                    Student = new Lecturer.Models.Student { Id="2", Name="User", Prename="Test", StudentMail = "student@student.howest.be" },
                    Status = Status.Approved
                },
                new RequestPartimInformation{
                    Id = 4,
                    Module = new Module { Code="4", Name="Module4" },
                    Partim = new Partim{ Code="4", Name="Partim4" },
                    Argumentation = "Test 4",
                    File = new File{ },
                    Evidence = new List<Evidence> { new Evidence { Description="Evidence4", Path="Evidence.png", StudentEmail = "student@student.howest.be" } }.AsEnumerable(),
                    Student = new Lecturer.Models.Student { Id="2", Name="User", Prename="Test", StudentMail = "student@student.howest.be" },
                    Status = Status.Rejected
                },
                new RequestPartimInformation{
                    Id = 5,
                    Module = new Module { Code="5", Name="Module5" },
                    Partim = new Partim{ Code="5", Name="Partim5" },
                    Argumentation = "Test 5",
                    File = new File{ },
                    Evidence = new List<Evidence> { new Evidence { Description="Evidence5", Path="Evidence.png", StudentEmail = "student@student.howest.be" } }.AsEnumerable(),
                    Student = new Lecturer.Models.Student { Id="2", Name="User", Prename="Test", StudentMail = "student@student.howest.be" },
                    Status = Status.Rejected
                }


            };
            mockdata = req.AsQueryable();
        }


        public IQueryable<RequestPartimInformation> GetRequests(string email, Status status)
        {

            return mockdata.Where(x => x.Status ==status );
        }

        public IQueryable<RequestPartimInformation> GetUntreadedRequestsDistinct(string email)
        {
            return mockdata.Where(x => x.Status == Status.Untreated);
        }

        public bool Approve(int requestId, bool isApproved, string email)
        {
            var requestPartimInformation = mockdata.Where(x => x.Id == requestId);
            Status s = requestPartimInformation.Select(x => x.Status).FirstOrDefault();

            if(s != Status.Untreated)
                return false;

            s = isApproved ? Status.Approved : Status.Rejected;
            requestPartimInformation.FirstOrDefault().Status = s;

            return true;
        }

        public bool hasAny(string email, Status status)
        {
            return mockdata.Any();
        }
    }
}
