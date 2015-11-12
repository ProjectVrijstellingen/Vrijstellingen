using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTP2015.ServiceLayer.Lecturer.Models;

namespace VTP2015.ServiceLayer.Lecturer
{
    public class MockLecturerFacade : ILecturerFacade
    {
        public IQueryable<RequestPartimInformation> GetUntreadedRequests(string email)
        {
            var req = new List<RequestPartimInformation> {
                new RequestPartimInformation
                {
                    Id = 1,
                    Module = new Module { },
                    Partim = new Partim{ },
                    Argumentation = "Test 1",
                    File = new File{ },
                    Evidence = new List<Evidence> { new Evidence { Description="Evidence1", Path="Evidence.png", StudentEmail="student@student.howest.be" } }.AsEnumerable()
                },
                new RequestPartimInformation{
                    Id = 2,
                    Module = new Module { },
                    Partim = new Partim{ },
                    Argumentation = "Test 2",
                    File = new File{ },
                    Evidence = new List<Evidence> { new Evidence { Description="Evidence2", Path="Evidence.png", StudentEmail = "student@student.howest.be" } }.AsEnumerable()
                }



            };
            
            return req.AsQueryable();
        }

        public IQueryable<RequestPartimInformation> GetUntreadedRequestsDistinct(string email)
        {
            var req = new List<RequestPartimInformation> {
                new RequestPartimInformation
                {
                    Id = 1,
                    Module = new Module { },
                    Partim = new Partim{ },
                    Argumentation = "Test 1",
                    File = new File{ }
                },
                new RequestPartimInformation{
                    Id = 2,
                    Module = new Module { },
                    Partim = new Partim{ },
                    Argumentation = "Test 2",
                    File = new File{ }
                }
                
            };

            return req.AsQueryable();
        }

        public bool Approve(int requestId, bool isApproved, string email)
        {
            return true;
        }
    }
}
