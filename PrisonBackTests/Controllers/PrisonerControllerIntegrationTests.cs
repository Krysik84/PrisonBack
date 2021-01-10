using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Management.Smo;
using NUnit.Framework;
using PrisonBack.Controllers;
using PrisonBack.Domain.Models;
using PrisonBack.Mapping;
using PrisonBack.Persistence.Context;
using PrisonBack.Persistence.Repositories;
using PrisonBack.Resources.DTOs;
using PrisonBack.Resources.ViewModels;
using PrisonBack.Services;

namespace PrisonBackTests.Controllers
{
    [TestFixture]
    class PrisonerControllerIntegrationTests
    {
        private static IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new ModelToResourceProfile()); });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;

        }

        [Test]
        public void IsAddingOnePrisoner()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_Add_Prisoner_database")
                .Options;

            var appDbContext = new AppDbContext(options);
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
            var prisonerRepository = new PrisonerRepository(appDbContext);
            var prisonerService = new PrisonerService(prisonerRepository);
            var loggerRepository = new LoggerRepository(appDbContext);
            var loggerService = new LoggerService(loggerRepository);
            var prisonerController = new PrisonerController(prisonerService, _mapper,loggerService)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } }
            };

            prisonerController.AddPrisoner(new PrisonerDTO
            {
                Name = "abc",
                Forname = "dba",
                Pesel = "12345678910",
                Address = "null",
                Pass = false,
                Behavior = 0,
                Isolated = false,
                IdCell = 0
            });

            Assert.AreEqual(appDbContext.Prisoners.Count(), 1, "Prisoner has not been added");
        }

        [Test]
        public void IsDeletingPrisoner()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_Delete_Prisoner_database")
                .Options;


            var appDbContext = new AppDbContext(options);
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
            var prisonerRepository = new PrisonerRepository(appDbContext);
            var prisonerService = new PrisonerService(prisonerRepository);
            var loggerRepository = new LoggerRepository(appDbContext);
            var loggerService = new LoggerService(loggerRepository);
            var prisonerController = new PrisonerController(prisonerService, _mapper, loggerService)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } }
            };


            prisonerController.AddPrisoner(new PrisonerDTO
            {
                Name = "abc",
                Forname = "dba",
                Pesel = "12345678910",
                Address = "null",
                Pass = false,
                Behavior = 0,
                Isolated = false,
                IdCell = 0
            });

            Assert.AreEqual(appDbContext.Prisoners.Count(), 1, "prisoner has not been added");
            prisonerController.DeletePrisoner(1);
            Assert.AreEqual(appDbContext.Prisoners.Count(), 0, "prisoner has not been deleted");

        }

        [Test]
        public void IsSelectingRightPrisoner()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_Select_Prisoner_database")
                .Options;


            var appDbContext = new AppDbContext(options);
            var prisonerRepository = new PrisonerRepository(appDbContext);
            var prisonerService = new PrisonerService(prisonerRepository);
            var loggerRepository = new LoggerRepository(appDbContext);
            var loggerService = new LoggerService(loggerRepository);
            var prisonerController = new PrisonerController(prisonerService, _mapper, loggerService);


            appDbContext.Prisoners.Add(new Prisoner
                {
                    Id = 0,
                    Name = null,
                    Forname = null,
                    Pesel = null,
                    Address = null,
                    Pass = false,
                    Behavior = 0,
                    Isolated = false,
                    IdCell = 0,
                    Cell = null,
                    Isolations = null,
                    Punishments = null
                });
            appDbContext.SaveChanges();

            Assert.AreEqual(appDbContext.Prisoners.Count(), 1, "prisoner has not been added");
            Assert.IsNotNull(prisonerController.SelectedPrisoner(1));

           // Assert.AreEqual(appDbContext.Prisoners.FirstOrDefault(x => x.Id == 1),prisonerController.SelectedPrisoner(1).Value);//rip c#7, gonna be fixed in c#8
        }

        [Test]
        public void IsUpdatingRightPrisoner()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_Update_Prisoner_database")
                .Options;


            var appDbContext = new AppDbContext(options);
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
            var prisonerRepository = new PrisonerRepository(appDbContext);
            var prisonerService = new PrisonerService(prisonerRepository);
            var loggerRepository = new LoggerRepository(appDbContext);
            var loggerService = new LoggerService(loggerRepository);
            var prisonerController = new PrisonerController(prisonerService, _mapper, loggerService)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } }
            };


            prisonerController.AddPrisoner(new PrisonerDTO
            {
                Name = "abc",
                Forname = "dba",
                Pesel = "12345678910",
                Address = "null",
                Pass = false,
                Behavior = 0,
                Isolated = false,
                IdCell = 1
            });
            var prisonerToUpdate = new PrisonerDTO
            {
                Name = "abc",
                Forname = "dba",
                Pesel = "12345678910",
                Address = "null",
                Pass = false,
                Behavior = 5,
                Isolated = true,
                IdCell = 3
            };
            Assert.AreEqual(appDbContext.Prisoners.Count(), 1, "prisoner has not been added");
            prisonerController.UpdatePrisoner(1, prisonerToUpdate);
            Assert.IsNotNull(appDbContext.Prisoners.FirstOrDefault(x => x.Id == 1));
            Assert.AreEqual(appDbContext.Prisoners.FirstOrDefault(x => x.Id == 1).IdCell,3);
            Assert.AreEqual(appDbContext.Prisoners.FirstOrDefault(x => x.Id == 1).IdCell, 3);
            Assert.AreEqual(appDbContext.Prisoners.FirstOrDefault(x => x.Id == 1).Behavior, 5);
            Assert.AreEqual(appDbContext.Prisoners.FirstOrDefault(x => x.Id == 1).Isolated, true);
        }
    }
}
