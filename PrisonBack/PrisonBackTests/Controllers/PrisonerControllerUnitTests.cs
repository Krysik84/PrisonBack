using AutoMapper;
using Moq;
using NUnit.Framework;
using PrisonBack.Controllers;
using PrisonBack.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PrisonBack.Domain.Models;
using PrisonBack.Resources.DTOs;
using PrisonBack.Resources.ViewModels;

namespace PrisonBackTests.Controllers
{
    [TestFixture]
    public class PrisonerControllerUnitTests
    {

        private Mock<IPrisonerService> _mockPrisonerService;
        private Mock<IMapper> _mockMapper;

        [SetUp]
        public void SetUp()
        {

            this._mockPrisonerService = new Mock<IPrisonerService>();
            this._mockMapper = new Mock<IMapper>();
        }

        private PrisonerController CreatePrisonerController()
        {
            return new PrisonerController(
                _mockPrisonerService.Object,
                _mockMapper.Object);
        }

        [Test]
        public void SelectedPrisonerUnitTests()
        {
            // Arrange
            var prisonerController = CreatePrisonerController();
            int id = 0;

            // Act
            var result = prisonerController.SelectedPrisoner(
                id);

            // Assert
            Assert.IsInstanceOf<ActionResult<PrisonerVM>>(result);
        }


        [Test]
        public void AddPrisonerUnitTests()
        {
            // Arrange
            var prisonerController = this.CreatePrisonerController();
            PrisonerDTO prisonerDto = new PrisonerDTO();

            // Act
            var result = prisonerController.AddPrisoner(
                prisonerDto);

            // Assert
            Assert.IsInstanceOf<ActionResult<PrisonerVM>>(result);
        }

        [Test]
        public void AllPrisonerUnitTests()
        {
            // Arrange
            var prisonerController = this.CreatePrisonerController();
            PrisonerDTO prisonerDto = new PrisonerDTO();

            // Act
            var result = prisonerController.AllPrisoner();

            // Assert
            Assert.IsInstanceOf<Task<IEnumerable<Prisoner>>>(result);
        }

        [Test]
        public void DeletePrisonerUnitTests()
        {
            // Arrange
            var prisonerController = CreatePrisonerController();
            int id = 0;

            // Act
            var result = prisonerController.DeletePrisoner(
                id);

            // Assert
            Assert.IsInstanceOf<ActionResult>(result);
        }

        [Test]
        public void UpdatePrisonerUnitTests()
        {
            // Arrange
            var prisonerController = CreatePrisonerController();
            int id = 0;
            PrisonerDTO prisonerDto = new PrisonerDTO();

            // Act
            var result = prisonerController.UpdatePrisoner(
                id,
                prisonerDto);

            // Assert
            Assert.IsInstanceOf<ActionResult>(result);
        }
    }
}
