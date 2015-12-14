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
        private List<File> _files;
        private List<Education> _educations;

        [SetUp]
        public void SetUp()
        {
            _files = new List<File>
            {
                new File
                {
                    Requests = new []
                    {
                        new Request { RequestPartimInformations = new []{ new RequestPartimInformation
                        {
                            PartimInformation = new PartimInformation { Module = new Module { Name = "m1"}, Partim = new Partim {Name = "p1"} },
                        } }},
                        new Request { RequestPartimInformations = new []{ new RequestPartimInformation
                        {
                            PartimInformation = new PartimInformation { Module = new Module { Name = "m1"}, Partim = new Partim {Name = "p2"} },
                        } }},
                        new Request { RequestPartimInformations = new []{ new RequestPartimInformation
                        {
                            PartimInformation = new PartimInformation { Module = new Module { Name = "m1"}, Partim = new Partim {Name = "p3"} },
                        } }},
                        new Request { RequestPartimInformations = new []{ new RequestPartimInformation
                        {
                            PartimInformation = new PartimInformation { Module = new Module { Name = "m2"}, Partim = new Partim {Name = "p3"} },
                        } }}
                    }
                }
            };

            _educations = new List<Education>
            {
                new Education { Counselors = new []{ new VTP2015.Entities.Counselor { Email = "" } }}
            };
        }

        [Test]
        public void Test_Modules_With_The_Same_Name_Are_Put_In_Same_Module()
        {
            // Setup
            var mockUnitOfWork = new MockUnitOfWork();
            mockUnitOfWork.AddResult(_files);
            
            var counselorFacade = new CounselorFacade(mockUnitOfWork);

            // Act
            var result = counselorFacade.GetFileByFileId(1);

            var amountOfModulesExpected = 2;
            var amountOfModulesActual = result.Modules.Count();

            // Assert
            Assert.AreEqual(amountOfModulesExpected, amountOfModulesActual);
        }
    }
}
