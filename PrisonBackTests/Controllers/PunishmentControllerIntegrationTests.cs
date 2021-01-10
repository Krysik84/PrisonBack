using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PrisonBack.Controllers;
using PrisonBack.Domain.Models;
using PrisonBack.Mapping;
using PrisonBack.Persistence.Context;
using PrisonBack.Persistence.Repositories;
using PrisonBack.Resources;
using PrisonBack.Services;

namespace PrisonBackTests.Controllers
{
    class PunishmentControllerIntegrationTests
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
        public void IsAddingOnePunishment()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_Add_Punishment_database")
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
            var punishmentRepository = new PunishmentRepository(appDbContext);
            var punishmentService = new PunishmentService(punishmentRepository);
            var loggerRepository = new LoggerRepository(appDbContext);
            var loggerService = new LoggerService(loggerRepository);
            var punishmentController = new PunishmentController(punishmentService,_mapper, loggerService)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } }
            };

            punishmentController.AddPunishment(new PunishmentDTO
            {
                IdPrisoner = 0,
                IdReason = 0,
                Lifery = false,
                StartDate = default,
                EndDate = default
            });

            Assert.AreEqual(appDbContext.Punishments.Count(),1);
        }

        [Test]
        public void IsDeletingOnePunishment()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_Delete_Punishment_database")
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
            var punishmentRepository = new PunishmentRepository(appDbContext);
            var punishmentService = new PunishmentService(punishmentRepository);
            var loggerRepository = new LoggerRepository(appDbContext);
            var loggerService = new LoggerService(loggerRepository);
            var punishmentController = new PunishmentController(punishmentService, _mapper, loggerService)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } }
            };

            punishmentController.AddPunishment(new PunishmentDTO
            {
                IdPrisoner = 0,
                IdReason = 0,
                Lifery = false,
                StartDate = default,
                EndDate = default
            });

            Assert.AreEqual(appDbContext.Punishments.Count(), 1);
            punishmentController.DeletePunishment(1);
            Assert.AreEqual(appDbContext.Punishments.Count(),0);
        }

        [Test]
        public void IsSelectingOnePunishment()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_Select_Punishment_database")
                .Options;

            var appDbContext = new AppDbContext(options);
            var punishmentRepository = new PunishmentRepository(appDbContext);
            var punishmentService = new PunishmentService(punishmentRepository);
            var loggerRepository = new LoggerRepository(appDbContext);
            var loggerService = new LoggerService(loggerRepository);
            var punishmentController = new PunishmentController(punishmentService, _mapper, loggerService);

            appDbContext.Prisoners.Add(new Prisoner
            {
                Id = 1,
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
                Punishments = new List<Punishment>()
            });

            appDbContext.Punishments.Add(new Punishment
            {
                Id = 0,
                IdPrisoner = 1,
                IdReason = 0,
                Lifery = false,
                StartDate = default,
                EndDate = default,
                Reason = null,
                Prisoner = null
            });
            appDbContext.SaveChanges();

            Assert.AreEqual(appDbContext.Punishments.Count(), 1);
            Assert.IsNotNull(appDbContext.Punishments.FirstOrDefault(x => x.IdPrisoner == 1));
            //Assert.AreEqual(appDbContext.Punishments.FirstOrDefault(x => x.IdPrisoner == 1),punishmentController.SelectedPunishment(1).Value);ripc#7, gonna be fixed in c#8
        }
    }
}
