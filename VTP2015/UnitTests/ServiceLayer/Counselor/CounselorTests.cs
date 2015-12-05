using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnitTests.Mock;
using VTP2015.Entities;
using VTP2015.ServiceLayer.Counselor;

namespace UnitTests.ServiceLayer.Counselor
{
    [TestFixture]
    class CounselorTests
    {
        private List<Request> _requests;

        [SetUp]
        public void SetUp()
        {
            _requests = new List<Request>
            {
                new Request
                {
                    File = new File { Student = new Student { Name = "Test"}},
                    RequestPartimInformations = new List<RequestPartimInformation>
                    {
                        new RequestPartimInformation
                        {
                            PartimInformation = new PartimInformation
                            {
                                Module = new Module {Name = "module1"},
                                Partim = new Partim { Name = "Partim1"}
                            },
                        },
                        new RequestPartimInformation
                        {
                            PartimInformation = new PartimInformation
                            {
                                Module = new Module {Name = "module1"},
                                Partim = new Partim { Name = "Partim1"}

                            },

                        }
                    },
                    Evidence = new List<Evidence>
                    {
                        new Evidence { Description = "Evidence1" },
                        new Evidence { Description = "Evidence2" }
                    }

                }
            };

        }

        [Test]
        public void Test_Modules_With_The_Same_Name_Are_Put_In_Same_Module()
        {
            // Setup
            var mockUnitOfWork = new MockUnitOfWork();
            mockUnitOfWork.AddResult(_requests);

            var counselorFacade = new CounselorFacade(mockUnitOfWork);

            // Act
            var result = counselorFacade.GetRequests();

            var amountOfModulesExpected = 1;
            var amountOfModulesActual = result.First().Modules.Count();

            // Assert
            Assert.AreEqual(amountOfModulesExpected, amountOfModulesActual);
        }

        [Test]
        public void Test_Modules_With_The_Same_Name_Are_Put_In_Different_Module()
        {
            // Setup
            var requests = new List<Request>(_requests);
            requests[0].RequestPartimInformations.First().PartimInformation.Module.Name = "Module2";

            var mockUnitOfWork = new MockUnitOfWork();
            mockUnitOfWork.AddResult(requests);

            var counselorFacade = new CounselorFacade(mockUnitOfWork);

            // Act
            var result = counselorFacade.GetRequests();

            var amountOfModulesExpected = 2;
            var amountOfModulesActual = result.First().Modules.Count();

            // Assert
            Assert.AreEqual(amountOfModulesExpected, amountOfModulesActual);
        }

        [Test]
        public void Test_Partims_With_Same_Module_Are_Correctly_Put_Into_Modules()
        {
            var mockUnitOfWork = new MockUnitOfWork();
            mockUnitOfWork.AddResult(_requests);

            var counselorFacade = new CounselorFacade(mockUnitOfWork);

            // Act
            var result = counselorFacade.GetRequests();

            var amountOfPartimsInFirstModuleExpected = 2;
            var amountOfPartimsInFirstModuleActual = result.First().Modules.First().Partims.Count();

            // Assert
            Assert.AreEqual(amountOfPartimsInFirstModuleExpected, amountOfPartimsInFirstModuleActual);
        }

        [Test]
        public void Test_Partims_With_DIfferent_Module_Are_Correctly_Put_Into_Modules()
        {
            // Setup
            var requests = new List<Request>(_requests);
            requests[0].RequestPartimInformations.First().PartimInformation.Module.Name = "Module2";

            var mockUnitOfWork = new MockUnitOfWork();
            mockUnitOfWork.AddResult(requests);

            var counselorFacade = new CounselorFacade(mockUnitOfWork);

            // Act
            var result = counselorFacade.GetRequests();

            var amountOfPartimsInFirstModuleExpected = 1;
            var amountOfPartimsInFirstModuleActual = result.First().Modules.First().Partims.Count();

            // Assert
            Assert.AreEqual(amountOfPartimsInFirstModuleExpected, amountOfPartimsInFirstModuleActual);
        }

        [Test]
        public void Test_Map_Evidence_In_Partims()
        {
            // Setup
            var mockUnitOfWork = new MockUnitOfWork();
            mockUnitOfWork.AddResult(_requests);

            var counselorFacade = new CounselorFacade(mockUnitOfWork);

            // Act
            var result = counselorFacade.GetRequests();

            var amountOfEvidenceExpected = 2;
            var amountOfEvidenceActual = result.First().Modules.First().Partims.First().Evidence.Count();

            // Assert
            Assert.AreEqual(amountOfEvidenceExpected, amountOfEvidenceActual);
        }
    }
}
