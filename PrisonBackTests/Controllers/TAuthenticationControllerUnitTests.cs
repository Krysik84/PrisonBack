using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using PrisonBack.Controllers;
using PrisonBack.Domain.Models;
using PrisonBack.Domain.Services;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PrisonBack.Auth;
using PrisonBack.Domain.Repositories;
using PrisonBack.Mailing;
using PrisonBack.Persistence.Context;
using PrisonBack.Persistence.Repositories;
using PrisonBack.Services;

namespace PrisonBackTests.Controllers
{
    [TestFixture]
    public class TAuthenticationControllerUnitTests
    {
        private Mock<UserManager<ApplicationUser>> _userManager;
        private Mock<RoleManager<IdentityRole>> _roleManager;
        private Mock<IConfiguration> _mockConfiguration;
        private Mock<IInviteCodeService> _mockInviteCodeService;
        private Mock<IAddUserRepository> _mockAddUserRepository;

        Mock<RoleManager<TIdentityRole>> GetRoleManagerMock<TIdentityRole>() where TIdentityRole : IdentityRole
        {
            return new Mock<RoleManager<TIdentityRole>>(
                new Mock<IRoleStore<TIdentityRole>>().Object,
                new IRoleValidator<TIdentityRole>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<ILogger<RoleManager<TIdentityRole>>>().Object);
        }
        Mock<UserManager<TIDentityUser>> GetUserManagerMock<TIDentityUser>() where TIDentityUser : IdentityUser
        {
            return new Mock<UserManager<TIDentityUser>>(
                new Mock<IUserStore<TIDentityUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<TIDentityUser>>().Object,
                new IUserValidator<TIDentityUser>[0],
                new IPasswordValidator<TIDentityUser>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<TIDentityUser>>>().Object);
        }
        [SetUp]
        public void SetUp()
        {
           
            _userManager = GetUserManagerMock<ApplicationUser>();
            _roleManager = GetRoleManagerMock<IdentityRole>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockInviteCodeService = new Mock<IInviteCodeService>();
            _mockAddUserRepository = new Mock<IAddUserRepository>();
        }
        
        private AuthenticationController CreateAuthenticationController()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_Add_Punishment_database")
                .Options;

            var appDbContext = new AppDbContext(options);
            return new AuthenticationController(
                _userManager.Object,
                _roleManager.Object,
                _mockConfiguration.Object,
                _mockInviteCodeService.Object,
                new AddUserService(new AddUserRepository(appDbContext)),
                new NotificationMail(new NotificationRepository(appDbContext)));
        }

        [Test]
        public async Task LoginUnitTest()
        {
            // Arrange
            var authenticationController = this.CreateAuthenticationController();
            LoginModel model = new LoginModel();

            // Act
            var result = await authenticationController.Login(
                model);

            // Assert
            _mockConfiguration.VerifyAll();
            _mockInviteCodeService.VerifyAll();
            _roleManager.VerifyAll();
            _userManager.VerifyAll();

        }

        [Test]
        public async Task RegisterUnitTests()
        {
            // Arrange
            var authenticationController = CreateAuthenticationController();
            RegisterModel model = new RegisterModel();

            // Act
            var result = await authenticationController.Register(
                model);

            // Assert
            _mockConfiguration.VerifyAll();
            _mockInviteCodeService.VerifyAll();
            _roleManager.VerifyAll();
            _userManager.VerifyAll();
        }

        [Test]
        public async Task RegisterAdminUnitTests()
        {
            // Arrange
            var authenticationController =CreateAuthenticationController();
            RegisterModel model = new RegisterModel();
            // Act
            // var result = await authenticationController.RegisterAdmin(
            //     model); TODO

            // Assert
            _mockConfiguration.VerifyAll();
            _mockInviteCodeService.VerifyAll();
            _roleManager.VerifyAll();
            _userManager.VerifyAll();
        }
    }
}
