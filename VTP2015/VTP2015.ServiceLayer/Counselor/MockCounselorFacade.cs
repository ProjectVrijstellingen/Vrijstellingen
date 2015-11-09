﻿using System;
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
                    AmountOfRequestsOpen = 100,
                    PercentageOfRequestsOpen = 50,
                    Route = "ssd",
                    StudentFirstName = "Sam",
                    StudentName = "De Creus"
                },

            }.AsQueryable();

            return files;
        }

        public void SendReminder(int aanvraagId)
        {
        }
    }
}
