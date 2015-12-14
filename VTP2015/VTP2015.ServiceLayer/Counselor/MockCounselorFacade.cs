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
            return new List<Request>().AsQueryable();
            //{
            //    new Request
            //    {
            //        Argumentation =
            //            "Lorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren '60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.",
            //        FileId = 1,
            //        RequestId = 1,
            //        Modules = new List<Module>
            //        {
            //            new Module
            //            {
            //                Name = "Module1",
            //                Partims = new List<Partim>
            //                {
            //                    new Partim
            //                    {
            //                        Name = "Partim1",
            //                        Status = Status.Approved,
            //                        Evidence = new List<Evidence>
            //                        {
            //                            new Evidence
            //                            {
            //                                Description =
            //                                    "Lorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren '60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.",
            //                                EvidenceId = 1,
            //                                Path = "pdf-test.pdf",
            //                                StudentEmail = "test@student.howest.be"
            //                            },
            //                            new Evidence
            //                            {
            //                                Description =
            //                                    "Lorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren '60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.",
            //                                EvidenceId = 1,
            //                                Path = "pdf-test.pdf",
            //                                StudentEmail = "test@student.howest.be"
            //                            }
            //                        },

            //                    },
            //                    new Partim
            //                    {
            //                        Name = "Partim1",
            //                        Status = Status.Rejected,
            //                        Evidence = new List<Evidence>
            //                        {
            //                            new Evidence
            //                            {
            //                                Description =
            //                                    "Lorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren '60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.",
            //                                EvidenceId = 1,
            //                                Path = "pdf-test.pdf",
            //                                StudentEmail = "test@student.howest.be"
            //                            },
            //                            new Evidence
            //                            {
            //                                Description =
            //                                    "Lorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren '60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.",
            //                                EvidenceId = 1,
            //                                Path = "pdf-test.pdf",
            //                                StudentEmail = "test@student.howest.be"
            //                            }
            //                        },

            //                    },
            //                    new Partim
            //                    {
            //                        Name = "Partim1",
            //                        Status = Status.Untreated,
            //                        Evidence = new List<Evidence>
            //                        {
            //                            new Evidence
            //                            {
            //                                Description =
            //                                    "Lorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren '60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.",
            //                                EvidenceId = 1,
            //                                Path = "pdf-test.pdf",
            //                                StudentEmail = "test@student.howest.be"
            //                            },
            //                            new Evidence
            //                            {
            //                                Description =
            //                                    "Lorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren '60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.",
            //                                EvidenceId = 1,
            //                                Path = "pdf-test.pdf",
            //                                StudentEmail = "test@student.howest.be"
            //                            }
            //                        },

            //                    }
            //                },
            //            },
            //            new Module
            //            {
            //                Name = "Module 2",
            //                Partims = new List<Partim>
            //                {
            //                    new Partim
            //                    {
            //                        Name = "Partim1",
            //                        Status = Status.Approved,
            //                        Evidence = new List<Evidence>
            //                        {
            //                            new Evidence
            //                            {
            //                                Description =
            //                                    "Lorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren '60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.",
            //                                EvidenceId = 1,
            //                                Path = "pdf-test.pdf",
            //                                StudentEmail = "test@student.howest.be"
            //                            },
            //                            new Evidence
            //                            {
            //                                Description =
            //                                    "Lorem Ipsum is slechts een proeftekst uit het drukkerij- en zetterijwezen. Lorem Ipsum is de standaard proeftekst in deze bedrijfstak sinds de 16e eeuw, toen een onbekende drukker een zethaak met letters nam en ze door elkaar husselde om een font-catalogus te maken. Het heeft niet alleen vijf eeuwen overleefd maar is ook, vrijwel onveranderd, overgenomen in elektronische letterzetting. Het is in de jaren '60 populair geworden met de introductie van Letraset vellen met Lorem Ipsum passages en meer recentelijk door desktop publishing software zoals Aldus PageMaker die versies van Lorem Ipsum bevatten.",
            //                                EvidenceId = 1,
            //                                Path = "pdf-test.pdf",
            //                                StudentEmail = "test@student.howest.be"
            //                            }
            //                        },
            //                    }

            //                }
            //            }
            //        }
            //    },

            //}.AsQueryable();
        }

        public File GetFileByFileId(int fileId)
        {
            return new File
            {
                StudentName = "Sam",
                Modules = new List<Module>
                {                
                    new Module { Name = "Module1", Partims = new List<Partim>
                    {
                        new Partim { Name = "Partim1", Evidence = new List<Evidence>
                        {
                            new Evidence { Description = "evidence1", Path = "/bewijzen/test/pdf-test.pdf"},
                            new Evidence { Description = "evidence2", Path = "/bewijzen/test/pdf-test.pdf"},
                        }, FileId = 1, RequestId = 1, Status = Status.Approved, Argumentation = "argumentation1"},
                        new Partim { Name = "Partim2", Evidence = new List<Evidence>{
                            new Evidence { Description = "evidence1", Path = "/bewijzen/test/pdf-test.pdf"},
                            new Evidence { Description = "evidence2", Path = "/bewijzen/test/pdf-test.pdf"},
                        }, FileId = 1, RequestId = 1, Status = Status.Rejected, Argumentation = "argumentation2"},
                        new Partim { Name = "Partim3", Evidence = new List<Evidence>{
                            new Evidence { Description = "evidence1", Path = "/bewijzen/test/pdf-test.pdf"},
                            new Evidence { Description = "evidence2", Path = "/bewijzen/test/pdf-test.pdf"},
                        }, FileId = 1, RequestId = 1, Status = Status.Untreated, Argumentation = "argumentation3"},
                    }},
                    new Module { Name = "Module2", Partims = new List<Partim>
                    {
                        new Partim { Name = "Partim4", Evidence = new List<Evidence>{
                            new Evidence { Description = "evidence1", Path = "/bewijzen/test/pdf-test.pdf"},
                            new Evidence { Description = "evidence2", Path = "path2"},
                        }, FileId = 1, RequestId = 1, Status = Status.Approved, Argumentation = "argumentation4"},
                        new Partim { Name = "Partim5", Evidence = new List<Evidence>{
                            new Evidence { Description = "evidence1", Path = "path1"},
                            new Evidence { Description = "evidence2", Path = "path2"},
                        }, FileId = 1, RequestId = 1, Status = Status.Rejected, Argumentation = "argumentation5"},
                    }},
                }
            };
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

        public IQueryable<File> GetFilesByCounselorEmail(string email, string academicYear)
        {
            var files = new List<File>
            {
                new File
                {
                    AcademicYear = "2015-16",
                    Id = 1,
                    AmountOfRequestsOpen = 3,
                    PercentageOfRequestsDone = 50,
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
                    PercentageOfRequestsDone = 0,
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
                    PercentageOfRequestsDone = 100,
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
                    PercentageOfRequestsDone = 90,
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
                    PercentageOfRequestsDone = 75,
                    Route = "ssd",
                    StudentFirstName = "Joske",
                    StudentName = "Vermeulen",
                    DateCreated = new DateTime(2015, 11, 08)
                },

            }.AsQueryable();

            return files;
        }

        public FileView GetFile(int fileId)
        {
            throw new NotImplementedException();
        }

        public void SendReminder(int aanvraagId)
        {
        }

        public void SetFileStatusOpen(int fileId)
        {
        }

        public void DeleteFile(int fileId)
        {
            
        }

        public bool IsFileAvailable(int fileId)
        {
            throw new NotImplementedException();
        }

        public void RemovePartimFromFile(int partimInformationId)
        {
        }

        public void RemovePartimFromFile(int partimInformationId, int fileId)
        {
            throw new NotImplementedException();
        }
    }
}
