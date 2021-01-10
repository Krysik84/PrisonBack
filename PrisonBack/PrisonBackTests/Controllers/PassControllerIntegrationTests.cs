using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "database_to_add_pass")
                .Options;

            var appDbContext = new AppDbContext(options);
            var passRepository = new PassRepository(appDbContext);
            var passService = new PassService(passRepository);
            var passController = new PassController(passService, _mapper);

            passController.AddPass(new PassDTO
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
                IdPrisoner = 1
            });
            Assert.AreEqual(appDbContext.Passes.Count(),1);
        }

        [Test]
        public void IsDeletingPass()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "database_to_delete_pass")
                .Options;

            var appDbContext = new AppDbContext(options);
            var passRepository = new PassRepository(appDbContext);
            var passService = new PassService(passRepository);

            var passController = new PassController(passService, _mapper);
            appDbContext.Prisoners.Add(new Prisoner
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
            });
            passController.AddPass(new PassDTO
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
                IdUser = 0,
                IdPrisoner = 1
            });


            passController.DeletePass(1);
            Assert.AreEqual(appDbContext.Passes.Count(),0,"pass was not deleted");

        }

        [Test]
        public void IsSelectingRightPass()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "database_to_select_pass")
                .Options;

            var appDbContext = new AppDbContext(options);
            var passRepository = new PassRepository(appDbContext);
            var passService = new PassService(passRepository);

            var passController = new PassController(passService, _mapper);
            appDbContext.Prisoners.Add(new Prisoner
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
            });
            passController.AddPass(new PassDTO
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
                IdUser = 0,
                IdPrisoner = 1
            });
            Assert.AreEqual(appDbContext.Passes.Count(), 1);

            Assert.IsTrue(appDbContext.Passes.Any(), "nothing here");
            Assert.IsNotNull(appDbContext.Passes.FirstOrDefault(x => x.Id == 1), "this pass is null");
            Assert.IsNotNull(passController.SelectedPass(1).Value,"selected pass return null value");
            Assert.AreEqual(appDbContext.Passes.FirstOrDefault(x => x.Id == 1), passController.SelectedPass(1).Value,"selected pass has different value");

        }

        [Test]
        public void IsUpdating()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "database_to_update_pass")
                .Options;

            var appDbContext = new AppDbContext(options);
            var passRepository = new PassRepository(appDbContext);
            var passService = new PassService(passRepository);

            var passController = new PassController(passService, _mapper);
            appDbContext.Prisoners.Add(new Prisoner
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
            });
            passController.AddPass(new PassDTO
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
                IdUser = 0,
                IdPrisoner = 1
            });

            var passToUpdate = new PassDTO
            {
                StartDate = DateTime.Today.AddDays(10),
                EndDate = DateTime.Today.AddDays(15),
                IdUser = 0,
                IdPrisoner = 1
            };

            passController.UpdatePass(1, passToUpdate);


            
            Assert.IsTrue(appDbContext.Passes.Any(),"nothing here");
            Assert.IsNotNull(appDbContext.Passes.FirstOrDefault(x=>x.Id==1),"this pass is null");
            // ReSharper disable once PossibleNullReferenceException, but checked up
            Assert.AreEqual(appDbContext.Passes.FirstOrDefault(x => x.Id == 1).StartDate,DateTime.Today.AddDays(10),"bad startDate");
            Assert.AreEqual(appDbContext.Passes.FirstOrDefault(x => x.Id == 1).IdPrisoner, 1,"something gone wrong with idPrisoner");
            Assert.AreEqual(appDbContext.Passes.FirstOrDefault(x => x.Id == 1).EndDate, DateTime.Today.AddDays(15),"bad endDate");

        }
    }
}
