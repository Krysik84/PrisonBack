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
using Microsoft.SqlServer.Management.Smo;
using NUnit.Framework.Internal;
using PrisonBack.Domain.Models;
using PrisonBack.Mapping;
using PrisonBack.Resources.DTOs;
using PrisonBack.Resources.ViewModels;

namespace PrisonBackTests.Controllers
{
    [TestFixture]
    public class PassControllerUnitTests
    {
        private Mock<IPassService> _mockPassService;
        private IMapper _mapper;
        private Mock<ILoggerService> _mockLoggerService;


        [SetUp]
        public void SetUp()
        {
            _mockPassService = new Mock<IPassService>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ModelToResourceProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
            _mockLoggerService = new Mock<ILoggerService>();
        }

        PassController CreatePassController()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "abcd")
            }));
            var passController = new PassController(
                _mockPassService.Object,
                _mapper,
                _mockLoggerService.Object)
            {
                ControllerContext = new ControllerContext()
            };
            passController.ControllerContext.HttpContext = new DefaultHttpContext{User = user};

            return passController;

        }

        [Test]
        public void SelectedPass_UnitTests()
        {
            // Arrange
            var passController = CreatePassController();
            int id = 0;

            // Act
            var result = passController.SelectedPass(
                id);

            // Assert
            Assert.IsInstanceOf<ActionResult<PassVM>>(result);
        }

        [Test]
        public async Task AllPasses_UnitTests()
        {
            // Arrange
            var passController = CreatePassController();
            int id = 0;
        
            // Act
            var result = await passController.AllPasses();

            // Assert
            Assert.IsInstanceOf<IEnumerable<Pass>>(result);
        }

        [Test]
        public void AddPass_UnitTests()
        {
            // Arrange
            var passController = CreatePassController();

            // Act
            var result = passController.AddPass(
                new PassDTO {EndDate = DateTime.Today.AddDays(1),IdPrisoner = 1,StartDate = DateTime.Today});

            // Assert
            Assert.IsInstanceOf<ActionResult<PassVM>>(result);
        }

        [Test]
        public void DeletePass_UnitTests()
        {
            // Arrange
            var passController = CreatePassController();
            int id = 0;

            // Act
            var result = passController.DeletePass(
                id);

            // Assert
            Assert.IsInstanceOf<ActionResult>(result);
        }

        [Test]
        public void UpdatePass_UnitTests()
        {
            // Arrange
            var passController = CreatePassController();
            int id = 0;
            PassDTO passDto = null;

            // Act
            var result = passController.UpdatePass(
                id, passDto);

            // Assert
            Assert.IsInstanceOf<ActionResult>(result);
        }
    }
}
