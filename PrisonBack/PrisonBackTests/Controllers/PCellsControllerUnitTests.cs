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
using PrisonBack.Resources;

namespace PrisonBackTests.Controllers
{
    [TestFixture]
    public class PCellsControllerUnitTests
    {

        private Mock<ICellService> _mockCellService;
        private Mock<IMapper> _mockMapper;
        private Mock<ILoggerService> _mockLoggerService;

        [SetUp]
        public void SetUp()
        {

            _mockCellService = new Mock<ICellService>();
            _mockMapper = new Mock<IMapper>();
            _mockLoggerService = new Mock<ILoggerService>();
        }

        private PCellsController CreatePCellsController()
        {
            return new PCellsController(
                _mockCellService.Object,
                _mockMapper.Object,
                _mockLoggerService.Object);
        }

        [Test]
        public void SelectedCellUnitTests()
        {
            // Arrange
            var pCellsController = this.CreatePCellsController();
            int id = 0;

            // Act
            var result = pCellsController.SelectedCell(
                id);

            // Assert
            Assert.IsInstanceOf<ActionResult<CellVM>>(result);
        }

        [Test]
        public async Task AllCellUnitTests()
        {
            // Arrange
            var pCellsController = this.CreatePCellsController();
            int id = 0;

            // Act
            var result = await pCellsController.AllCell(
                id);

            // Assert
            Assert.IsInstanceOf<IEnumerable<Cell>>(result);
        }

        [Test]
        public void AddCellUnitTests()
        {
            // Arrange
            var pCellsController = this.CreatePCellsController();
            CellDTO cellDto = new CellDTO();

            // Act
            var result = pCellsController.AddCell(
                cellDto);

            // Assert
            Assert.IsInstanceOf<ActionResult<CellVM>>(result);
        }

        [Test]
        public void DeleteCellUnitTests()
        {
            // Arrange
            var pCellsController = this.CreatePCellsController();
            int id = 0;

            // Act
            var result = pCellsController.DeleteCell(
                id);

            // Assert
            Assert.IsInstanceOf<ActionResult>(result);
        }

        [Test]
        public void UpdateCellUnitTests()
        {
            // Arrange
            var pCellsController = this.CreatePCellsController();
            int id = 0;
            CellDTO cellDto =new CellDTO();

            // Act
            var result = pCellsController.UpdateCell(
                id,
                cellDto);

            // Assert
            Assert.IsInstanceOf<ActionResult>(result);
        }
    }
}
