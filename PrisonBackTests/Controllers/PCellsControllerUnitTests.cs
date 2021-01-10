using AutoMapper;
using Moq;
using NUnit.Framework;
using PrisonBack.Controllers;
using PrisonBack.Domain.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrisonBack.Domain.Models;
using PrisonBack.Mapping;
using PrisonBack.Persistence.Context;
using PrisonBack.Persistence.Repositories;
using PrisonBack.Resources;
using PrisonBack.Services;

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

        PCellsController CreatePCellsController()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "abcd")
            }));
            var pCellsController = new PCellsController(
                _mockCellService.Object,
                _mockMapper.Object,
                _mockLoggerService.Object);
            pCellsController.ControllerContext = new ControllerContext();
            pCellsController.ControllerContext.HttpContext = new DefaultHttpContext
            {
                User = user
            };
            return pCellsController;
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
            var result = await pCellsController.AllCell();

            // Assert
            Assert.IsInstanceOf<IEnumerable<Cell>>(result);
        }

        [Test]
        public void AddCellUnitTests()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "database_to_add_cell")
                .Options;

            var appDbContext = new AppDbContext(options);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ModelToResourceProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "abcd"),
            }));
            appDbContext.UserPermissions.Add(new UserPermission
            {
                Id = 1,
                UserName = "abcd",
                IdPrison = 1,
                Prison = new Prison
                {
                    Id = 1,
                    PrisonName = "prison_test"
                }
            });
            appDbContext.SaveChanges();
            var loggerRepository = new LoggerRepository(appDbContext);
            var loggerService = new LoggerService(loggerRepository);

            var cellRepository = new CellRepository(appDbContext);
            var cellService = new CellService(cellRepository);

            var pCellsController = new PCellsController(
                cellService,
                mapper,
                loggerService)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } }
            };

            // Act
            var result = pCellsController.AddCell(new CellDTO());

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
