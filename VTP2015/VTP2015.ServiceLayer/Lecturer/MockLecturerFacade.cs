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
                    Evidence = new List<Evidence> { new Evidence { Description="Evidence1", Path="Evidence.png" } }.AsQueryable(),
                    Student = new Lecturer.Models.Student { Id="1", Name="Gebruiker", FirstName="Test", Email = "test@student.howest.be" },
                    Status = Status.Untreated
                },
                new RequestPartimInformation{
                    Id = 2,
                    Module = new Module { Code="2", Name="Module2" },
                    Partim = new Partim{ Code="2", Name="Partim2" },
                    Argumentation = "Test 2",
                    Evidence = new List<Evidence> { new Evidence { Description="Evidence2", Path="Evidence.png"} }.AsQueryable(),
                    Student = new Lecturer.Models.Student { Id="2", Name="User", FirstName="Test", Email = "student@student.howest.be" },
                    Status = Status.Untreated
                },
                new RequestPartimInformation{
                    Id = 3,
                    Module = new Module { Code="3", Name="Module3" },
                    Partim = new Partim{ Code="3", Name="Partim3" },
                    Argumentation = "Test 3",
                    Evidence = new List<Evidence> { new Evidence { Description="Evidence3", Path="Evidence.png"} }.AsQueryable(),
                    Student = new Lecturer.Models.Student { Id="2", Name="User", FirstName="Test", Email = "student@student.howest.be" },
                    Status = Status.Approved
                },
                new RequestPartimInformation{
                    Id = 4,
                    Module = new Module { Code="4", Name="Module4" },
                    Partim = new Partim{ Code="4", Name="Partim4" },
                    Argumentation = "Test 4",
                    Evidence = new List<Evidence> { new Evidence { Description="Evidence4", Path="Evidence.png"} }.AsQueryable(),
                    Student = new Lecturer.Models.Student { Id="2", Name="User", FirstName="Test", Email = "student@student.howest.be" },
                    Status = Status.Rejected
                },
                new RequestPartimInformation{
                    Id = 5,
                    Module = new Module { Code="5", Name="Module5" },
                    Partim = new Partim{ Code="5", Name="Partim5" },
                    Argumentation = "Test 5",
                    Evidence = new List<Evidence> { new Evidence { Description="Evidence5", Path="Evidence.png"} }.AsQueryable(),
                    Student = new Lecturer.Models.Student { Id="2", Name="User", FirstName="Test", Email = "student@student.howest.be" },
                    Status = Status.Rejected
                },
                new RequestPartimInformation{
                    Id = 6,
                    Module = new Module { Code="2", Name="Module2" },
                    Partim = new Partim{ Code="2", Name="Partim2" },
                    Argumentation = "Test 2",
                    Evidence = new List<Evidence> { new Evidence { Description="Evidence1", Path="Evidence.png" } }.AsQueryable(),
                    Student = new Lecturer.Models.Student { Id="1", Name="Gebruiker", FirstName="Test", Email = "test@student.howest.be" },
                    Status = Status.Untreated
                }


            };
            mockdata = req.AsQueryable();
        }


        public IQueryable<RequestPartimInformation> GetRequests(string email, Status status)
        {

            return mockdata.Where(x => x.Status ==status );
        }

        public IQueryable<Models.Student> GetUntreadedStudent(string email)
        {
            return null; // mockdata.Where(x => x.Status == Status.Untreated).GroupBy(s => s.Student.Id).Select(grp => grp.First());
        }

        public bool Approve(int requestId, bool isApproved, string email, int motivationId)
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

        public IQueryable<PartimInformation> GetPartims(string email)
        {
            return null; //mockdata.GroupBy(s => s.Partim).Select(grp => grp.First());
        }

        public IQueryable<Motivation> GetMotivations()
        {
            throw new NotImplementedException();
        }

        public bool RemovePartimLecturer(string supercode)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Models.Student> GetTreadedStudent(string email)
        {
            throw new NotImplementedException();
        }

        public int getAantal(string email, Status status)
        {
            throw new NotImplementedException();
        }
    }
}
