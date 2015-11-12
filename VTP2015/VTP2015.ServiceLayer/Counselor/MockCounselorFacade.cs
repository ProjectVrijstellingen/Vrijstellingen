using System;
using System.Collections.Generic;
using System.Linq;
using VTP2015.ServiceLayer.Counselor.Models;

namespace VTP2015.ServiceLayer.Counselor
{
    public class MockCounselorFacade : ICounselorFacade
    {
        public IQueryable<Request> GetRequests()
        {
            return new List<Request>
            {
                new Request
                {
                    Argumentation = "argumentation",
                    Status = Status.Untreated,
                    Evidence = new List<Evidence>().AsQueryable(),
                    FileId = 1,
                    ModuleName = "Software Development",
                    PartimName = "c#",
                    RequestId = 1
                }
            }.AsQueryable();
        }

        public string GetEducationNameByCounselorEmail(string email)
        {
            return "Toegepaste Informatica";
        }

        public IQueryable<Education> GetEducations()
        {
            return new List<Education>
            {
                new Education {Id = 1, Name = "Toegepaste Informatica"}
            }.AsQueryable();
        }

        public void ChangeEducation(string email, string educationName)
        {
        }

        public IQueryable<File> GetFileByCounselorEmail(string email, string academicYear)
        {
            var files = new List<File>
            {
                new File
                {
                    AcademicYear = "2015-16",
                    Id = 1,
                    AmountOfRequestsOpen = 3,
                    PercentageOfRequestsOpen = 50,
                    Route = "ssd",
                    StudentFirstName = "Sam",
                    StudentName = "De Creus",
                    DateCreated = Convert.ToDateTime("01-11-2015 08:05:07")
                },
                new File
                {
                    AcademicYear = "2015-16",
                    Id = 2,
                    AmountOfRequestsOpen = 3,
                    PercentageOfRequestsOpen = 75,
                    Route = "cccp",
                    StudentFirstName = "Toon",
                    StudentName = "Swyzen",
                    DateCreated = Convert.ToDateTime("23-10-2015 08:05:07")
                },
                new File
                {
                    AcademicYear = "2015-16",
                    Id = 3,
                    AmountOfRequestsOpen = 8,
                    PercentageOfRequestsOpen = 100,
                    Route = "cccp",
                    StudentFirstName = "Joachim",
                    StudentName = "Bockland",
                    DateCreated = Convert.ToDateTime("27-10-2015 08:05:07")
                },
                new File
                {
                    AcademicYear = "2015-16",
                    Id = 4,
                    AmountOfRequestsOpen = 9,
                    PercentageOfRequestsOpen = 90,
                    Route = "ssd",
                    StudentFirstName = "Olivier",
                    StudentName = "Sourie",
                    DateCreated = Convert.ToDateTime("04-11-2015 08:05:07")
                },
                new File
                {
                    AcademicYear = "2015-16",
                    Id = 5,
                    AmountOfRequestsOpen = 0,
                    PercentageOfRequestsOpen = 0,
                    Route = "ssd",
                    StudentFirstName = "Joske",
                    StudentName = "Vermeulen",
                    DateCreated = Convert.ToDateTime("10-11-2015 08:05:07")
                },

            }.AsQueryable();

            return files;
        }

        public void SendReminder(int aanvraagId)
        {
        }
    }
}
