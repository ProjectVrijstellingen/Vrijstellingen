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
            return new List<Request>().AsQueryable();
            //return new List<Request>
            //{
            //    new Request
            //    {
            //        Argumentation = "Lorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren '60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.",
            //        Status = Status.Untreated,
            //        Evidence = new List<Evidence>
            //        {
            //            new Evidence
            //            {
            //                Description = "Lorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren '60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.",
            //                EvidenceId = 1,
            //                Path = "pdf-test.pdf",
            //                StudentEmail = "test@student.howest.be"
            //            },
            //            new Evidence
            //            {
            //                Description = "Lorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren '60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.",
            //                EvidenceId = 1,
            //                Path = "pdf-test.pdf",
            //                StudentEmail = "test@student.howest.be"
            //            },
            //        }.AsQueryable(),
            //        FileId = 1,
            //        ModuleName = "Software Development",
            //        PartimName = "c#",
            //        RequestId = 1
            //    },
            //    new Request
            //    {
            //        Argumentation = "Lorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren '60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.",
            //        Status = Status.Untreated,
            //        Evidence = new List<Evidence>().AsQueryable(),
            //        FileId = 1,
            //        ModuleName = "Software Development",
            //        PartimName = "c#",
            //        RequestId = 2
            //    },
            //    new Request
            //    {
            //        Argumentation = "Lorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren '60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.",
            //        Status = Status.Untreated,
            //        Evidence = new List<Evidence>().AsQueryable(),
            //        FileId = 1,
            //        ModuleName = "Software Development",
            //        PartimName = "c#",
            //        RequestId = 3
            //    },
            //    new Request
            //    {
            //        Argumentation = "Lorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren '60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.",
            //        Status = Status.Untreated,
            //        Evidence = new List<Evidence>().AsQueryable(),
            //        FileId = 1,
            //        ModuleName = "Software Development",
            //        PartimName = "c#",
            //        RequestId = 4
            //    },
            //    new Request
            //    {
            //        Argumentation = "Lorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren '60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.",
            //        Status = Status.Rejected,
            //        Evidence = new List<Evidence>().AsQueryable(),
            //        FileId = 1,
            //        ModuleName = "Software Development",
            //        PartimName = "c#",
            //        RequestId = 5
            //    },
            //    new Request
            //    {
            //        Argumentation = "Lorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren '60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.",
            //        Status = Status.Approved,
            //        Evidence = new List<Evidence>().AsQueryable(),
            //        FileId = 1,
            //        ModuleName = "Software Development",
            //        PartimName = "c#",
            //        RequestId = 5
            //    },
            //    new Request
            //    {
            //        Argumentation = "Lorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren '60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.",
            //        Status = Status.Rejected,
            //        Evidence = new List<Evidence>().AsQueryable(),
            //        FileId = 1,
            //        ModuleName = "Software Development",
            //        PartimName = "c#",
            //        RequestId = 5
            //    },
            //}.AsQueryable();
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
                    AmountOfRequestsOpen = 0,
                    PercentageOfRequestsOpen = 0,
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
                    AmountOfRequestsOpen = 3,
                    PercentageOfRequestsOpen = 75,
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
