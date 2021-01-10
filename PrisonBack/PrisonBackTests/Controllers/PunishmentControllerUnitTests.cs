using AutoMapper;
using Moq;
using NUnit.Framework;
using PrisonBack.Controllers;
using PrisonBack.Domain.Services;
using System;
using PrisonBack.Resources;

namespace PrisonBackTests.Controllers
{
    [TestFixture]
    public class PunishmentControllerUnitTests
    {

        private Mock<IPunishmentService> _mockPunishmentService;
        private Mock<IMapper> _mockMapper;

        [SetUp]
        public void SetUp()
        {

            this._mockPunishmentService = new Mock<IPunishmentService>();
            this._mockMapper = new Mock<IMapper>();
        }

        private PunishmentController CreatePunishmentController()
        {
            return new PunishmentController(
                this._mockPunishmentService.Object,
                this._mockMapper.Object);
        }

        [Test]
        public void SelectedPunishmentUnitTests()
        {
            // Arrange
            var punishmentController = this.CreatePunishmentController();
            int id = 0;

            // Act
            var result = punishmentController.SelectedPunishment(
                id);

            // Assert
            _mockPunishmentService.VerifyAll();
        }

        [Test]
        public void AddPunishmentUnitTests()
        {
            // Arrange
            var punishmentController = this.CreatePunishmentController();
            PunishmentDTO punishmentDto = new PunishmentDTO();

            // Act
            var result = punishmentController.AddPunishment(
                punishmentDto);

            // Assert
            _mockPunishmentService.VerifyAll();
        }

        [Test]
        public void DeletePunishmentUnitTests()
        {
            // Arrange
            var punishmentController = this.CreatePunishmentController();
            int id = 0;

            // Act
            var result = punishmentController.DeletePunishment(
                id);

            // Assert
            _mockPunishmentService.VerifyAll();
        }

        [Test]
        public void UpdatePunishmentUnitTests()
        {
            // Arrange
            var punishmentController = this.CreatePunishmentController();
            int id = 0;
            PunishmentDTO punishmentDto = new PunishmentDTO();

            // Act
            var result = punishmentController.UpdatePunishment(
                id,
                punishmentDto);

            // Assert
            _mockPunishmentService.VerifyAll();
        }
    }
}
