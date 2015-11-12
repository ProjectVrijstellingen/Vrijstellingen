using System;
using System.Collections.Generic;
using System.Globalization;
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
                    DateCreated = new DateTime(2015, 10, 23)
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
                    DateCreated = new DateTime(2015, 11, 01)
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
                    DateCreated = new DateTime(2015, 11, 05)
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
                    DateCreated = new DateTime(2015, 10, 28)
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
                    DateCreated = new DateTime(2015, 11, 08)
                },

            }.AsQueryable();

            return files;
        }

        public void SendReminder(int aanvraagId)
        {
        }
    }
}
