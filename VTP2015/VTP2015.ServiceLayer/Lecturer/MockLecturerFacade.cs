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
            throw new NotImplementedException();
        }

        public IQueryable<RequestPartimInformation> GetUntreadedRequestsDistinct(string email)
        {
            throw new NotImplementedException();
        }

        public bool Approve(int requestId, bool isApproved, string email)
        {
            throw new NotImplementedException();
        }
    }
}
