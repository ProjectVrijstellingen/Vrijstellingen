using System;
using System.Collections.Generic;
using System.Linq;
using VTP2015.ServiceLayer.Counselor.Models;

namespace VTP2015.ServiceLayer.Counselor
{
    public class MockCounselorFacade : ICounselorFacade
    {
        public IQueryable<RequestPartimInformation> GetRequests()
        {
            return new List<RequestPartimInformation>
            {
                new RequestPartimInformation
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
                    StudentName = "De Creus"
                },
                new File
                {
                    AcademicYear = "2015-16",
                    Id = 2,
                    AmountOfRequestsOpen = 3,
                    PercentageOfRequestsOpen = 75,
                    Route = "cccp",
                    StudentFirstName = "Toon",
                    StudentName = "Swyzen"
                },
                new File
                {
                    AcademicYear = "2015-16",
                    Id = 3,
                    AmountOfRequestsOpen = 8,
                    PercentageOfRequestsOpen = 100,
                    Route = "cccp",
                    StudentFirstName = "Joachim",
                    StudentName = "Bockland"
                },
                new File
                {
                    AcademicYear = "2015-16",
                    Id = 4,
                    AmountOfRequestsOpen = 9,
                    PercentageOfRequestsOpen = 90,
                    Route = "ssd",
                    StudentFirstName = "Olivier",
                    StudentName = "Souri"
                },
                new File
                {
                    AcademicYear = "2015-16",
                    Id = 5,
                    AmountOfRequestsOpen = 0,
                    PercentageOfRequestsOpen = 0,
                    Route = "ssd",
                    StudentFirstName = "Joske",
                    StudentName = "Vermeulen"
                },

            }.AsQueryable();

            return files;
        }

        public void SendReminder(int aanvraagId)
        {
        }
    }
}
