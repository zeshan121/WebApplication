using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyWebApp.Core.Interfaces;
using MyWebApp.Web.Controllers;
using MyWebApp.Core.Entities;
using MyWebApp.Web.Models.GamingMachine;
using System.Web.Mvc;
using MyWebApp.Core.Entities.Result;
using MyWebApp.Core.Services;
using MyWebApp.Core.Specifications;
using System.Collections.Generic;
using System.Linq;

namespace MyWebApp.Tests
{
    [TestClass]
    public class GamingMachineServiceUnitTest
    {
        private Mock<IRepository<GamingMachine>> mockGamingMachineRepository;
        [TestInitialize]
        public void SetupTests()
        {
            mockGamingMachineRepository = new Mock<IRepository<GamingMachine>>(MockBehavior.Strict);

        }

        [TestMethod]
        public void GamingMachineService_Create_Success()
        {
            // Arrange
            GamingMachineService gamingMachineService = new GamingMachineService(mockGamingMachineRepository.Object);
            GamingMachine gamingMachine =
                new GamingMachine()
                {
                    GamingSerialNumber = 3000,
                    GamingMachinePosition = 1000,
                    GameName = "Game_3000"
                };

            mockGamingMachineRepository.Setup(m => m.List(It.IsAny<ISpecification<GamingMachine>>())).Returns(new List<GamingMachine>());
            mockGamingMachineRepository.Setup(m => m.Add(gamingMachine));

            // Act
            Result result = gamingMachineService.CreateGamingMachine(gamingMachine);

            // Assert
            Assert.IsTrue(result.Succeeded);

        }
        [TestMethod]
        public void GamingMachineService_Create_NullMachineName_Failed()
        {
            // Arrange
            GamingMachineService gamingMachineService = new GamingMachineService(mockGamingMachineRepository.Object);
            GamingMachine gamingMachine =
                new GamingMachine()
                {
                    GamingSerialNumber = 1,
                    GamingMachinePosition = 1,
                    GameName = null
                };

            mockGamingMachineRepository.Setup(m => m.Add(It.IsAny<GamingMachine>()));

            // Act
            Result result = gamingMachineService.CreateGamingMachine(gamingMachine);

            // Assert
            Assert.IsFalse(result.Succeeded);


        }
        [TestMethod]
        public void GamingMachineService_Delete_Success()
        {
            // Arrange
            GamingMachineService gamingMachineService = new GamingMachineService(mockGamingMachineRepository.Object);
            GamingMachine gamingMachine =
                new GamingMachine()
                {
                    GamingSerialNumber = 1,
                    GamingMachinePosition = 1,
                    GameName = "Game_1"
                };

            mockGamingMachineRepository.Setup(m => m.Delete(It.IsAny<GamingMachine>()));

            // Act
            Result result = gamingMachineService.DeleteGamingMachine(gamingMachine);

            // Assert
            Assert.IsTrue(result.Succeeded);

        }

        [TestMethod]
        public void GamingMachineService_Get_MachineBySerialNumber_Success()
        {
            // Arrange
            GamingMachineService gamingMachineService = new GamingMachineService(mockGamingMachineRepository.Object);
            List<GamingMachine> gamingMachine = new List<GamingMachine> {
                new GamingMachine
                {
                    GamingSerialNumber = 1,
                    GamingMachinePosition = 1,
                    GameName = "Game_1"
                },
                new GamingMachine
                {
                    GamingSerialNumber = 2,
                    GamingMachinePosition = 2,
                    GameName = "Game_2"
                }
            };

            mockGamingMachineRepository.Setup(m => m.List(It.IsAny<ISpecification<GamingMachine>>())).Returns(gamingMachine.Where(g => g.GamingSerialNumber == 2).ToList());

            // Act
            GamingMachine result = gamingMachineService.Get(gamingMachine[0].GamingSerialNumber);

            // Assert
            Assert.AreEqual(result.GameName, "Game_2");

        }

    }
}
