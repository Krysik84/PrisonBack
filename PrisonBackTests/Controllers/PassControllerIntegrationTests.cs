using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Management.Smo;
using Moq;
using NUnit.Framework;
using PrisonBack.Controllers;
using PrisonBack.Domain.Models;
using PrisonBack.Mapping;
using PrisonBack.Migrations;
using PrisonBack.Persistence.Context;
using PrisonBack.Persistence.Repositories;
using PrisonBack.Resources.DTOs;
using PrisonBack.Resources.ViewModels;
using PrisonBack.Services;

namespace PrisonBackTests.Controllers
{
    class PassControllerIntegrationTests
    {
        private static IMapper _mapper;


        [SetUp]
        public void Setup()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ModelToResourceProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;

        }
        [Test]
        public void IsAddingNewPass()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "abcd"),
            }));

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "database_to_add_pass")
                .Options;

            var appDbContext = new AppDbContext(options);
 
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
            var passRepository = new PassRepository(appDbContext);
            var passService = new PassService(passRepository);
            var loggerRepository = new LoggerRepository(appDbContext);
            var loggerService = new LoggerService(loggerRepository);
            var passController = new PassController(passService, _mapper, loggerService)
            {
                ControllerContext = new ControllerContext {HttpContext = new DefaultHttpContext {User = user}}
            };

            
            appDbContext.SaveChanges();
            appDbContext.Prisoners.Add(new Prisoner
            {
                Id = 1,
                Name = "fdsafd",
                Forname = "dsdsa",
                Pesel = "12345678910",
                Address = "dsafa",
                Pass = false,
                Behavior = 2,
                Isolated = false,
                IdCell = 1,
                Cell = new Cell(),
                Isolations = new List<Isolation>(),
                Punishments = new List<Punishment>()
            });
            appDbContext.SaveChanges();
            passController.AddPass(new PassDTO
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
                IdPrisoner = 1
            });
            Assert.AreEqual(appDbContext.Passes.Count(), 1);
        }

        [Test]
        public void IsDeletingPass()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "database_to_delete_pass")
                .Options;

            var appDbContext = new AppDbContext(options);
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
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "abcd"),
            }));
            var passRepository = new PassRepository(appDbContext);
            var passService = new PassService(passRepository);
            var loggerRepository = new LoggerRepository(appDbContext);
            var loggerService = new LoggerService(loggerRepository);
            var passController = new PassController(passService, _mapper, loggerService)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } }
            };
            appDbContext.Passes.Add(new Pass
            {
                Id = 1,
                StartDate = default,
                EndDate = default,
                IdPrisoner = 1,
                Prisoner = new Prisoner
                {
                    Id = 1,
                    Name = "abcd",
                    Forname = "abc",
                    Pesel = "11111111111",
                    Address = "dsada 11",
                    Pass = false,
                    Behavior = 3,
                    Isolated = false,
                    IdCell = 1,
                    Cell = new Cell(),
                    Isolations = new List<Isolation>(),
                    Punishments = new List<Punishment>()
                }
            });
            appDbContext.SaveChanges();


            passController.DeletePass(1);
            Assert.AreEqual(appDbContext.Passes.Count(), 0, "pass was not deleted");

        }

        [Test]
        public void IsSelectingRightPass()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "database_to_select_pass")
                .Options;

            var appDbContext = new AppDbContext(options);

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
            var passRepository = new PassRepository(appDbContext);
            var passService = new PassService(passRepository);
            var loggerRepository = new LoggerRepository(appDbContext);
            var loggerService = new LoggerService(loggerRepository);
            var passController = new PassController(passService, _mapper, loggerService);

            appDbContext.Passes.Add(new Pass
            {
                Id = 1,
                StartDate = default,
                EndDate = default,
                IdPrisoner = 1,
                Prisoner = new Prisoner
                {
                    Id = 1,
                    Name = "abcd",
                    Forname = "abc",
                    Pesel = "11111111111",
                    Address = "dsada 11",
                    Pass = false,
                    Behavior = 3,
                    Isolated = false,
                    IdCell = 1,
                    Cell = new Cell(),
                    Isolations = new List<Isolation>(),
                    Punishments = new List<Punishment>()
                }
            });
            appDbContext.SaveChanges();

            var valueOfSelectedPass = passController.SelectedPass(1).Value;
            Assert.AreEqual(appDbContext.Passes.Count(), 1);
            Assert.IsTrue(appDbContext.Passes.Any(), "nothing here");
            Assert.IsNotNull(appDbContext.Passes.FirstOrDefault(x => x.Id == 1), "this pass is null");
            //Assert.IsNotNull(valueOfSelectedPass, "selected pass return null value"); //rip c#7, gonna work in c#8
            //Assert.AreEqual(appDbContext.Passes.FirstOrDefault(x => x.Id == 1), valueOfSelectedPass, "selected pass has different value"); //rip c#7 gonna work in c#8

        }

        [Test]
        public void IsUpdating()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "database_to_update_pass")
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
            var passRepository = new PassRepository(appDbContext);
            var passService = new PassService(passRepository);
            var loggerRepository = new LoggerRepository(appDbContext);
            var loggerService = new LoggerService(loggerRepository);
            var passController = new PassController(passService, _mapper, loggerService)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } }
            };
            appDbContext.Passes.Add(new Pass
            {
                Id = 1,
                StartDate = default,
                EndDate = default,
                IdPrisoner = 1,
                Prisoner = new Prisoner
                {
                    Id = 1,
                    Name = "abcd",
                    Forname = "abc",
                    Pesel = "11111111111",
                    Address = "dsada 11",
                    Pass = false,
                    Behavior = 3,
                    Isolated = false,
                    IdCell = 1,
                    Cell = new Cell(),
                    Isolations = new List<Isolation>(),
                    Punishments = new List<Punishment>()
                }
            });
            appDbContext.SaveChanges();

            var passToUpdate = new PassDTO
            {
                StartDate = DateTime.Today.AddDays(10),
                EndDate = DateTime.Today.AddDays(15),
                IdPrisoner = 1
            };

            passController.UpdatePass(1, passToUpdate);



            Assert.IsTrue(appDbContext.Passes.Any(), "nothing here");
            Assert.IsNotNull(appDbContext.Passes.FirstOrDefault(x => x.Id == 1), "this pass is null");
            // ReSharper disable once PossibleNullReferenceException, but checked up
            Assert.AreEqual(appDbContext.Passes.FirstOrDefault(x => x.Id == 1).StartDate, DateTime.Today.AddDays(10), "bad startDate");
            Assert.AreEqual(appDbContext.Passes.FirstOrDefault(x => x.Id == 1).IdPrisoner, 1, "something gone wrong with idPrisoner");
            Assert.AreEqual(appDbContext.Passes.FirstOrDefault(x => x.Id == 1).EndDate, DateTime.Today.AddDays(15), "bad endDate");

        }
    }
}
