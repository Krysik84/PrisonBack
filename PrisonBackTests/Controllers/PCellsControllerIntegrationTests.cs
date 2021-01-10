using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Management.Smo;
using Moq;
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
    [TestFixture]
    class PCellsControllerIntegrationTests
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
        public void IsCellAdded()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "database_to_add_cell")
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
            var loggerRepository = new LoggerRepository(appDbContext);
            var loggerService = new LoggerService(loggerRepository);

            var cellRepository = new CellRepository(appDbContext);
            var cellService = new CellService(cellRepository);

            var cellController = new PCellsController(
                cellService,
                _mapper,
                loggerService)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } }
            };


            var cell1 = new CellDTO
            {
                Beds = 1,
                IdCellType = 1,
            };

            Assert.IsFalse(appDbContext.Cells.Any(),"Cell was added before it should be");
            cellController.AddCell(cell1);
            Assert.AreEqual(1,appDbContext.Cells.Count(),"amount of cells is wrong");
        }
        [Test]
         public void IsSelectingRight()
         {
             var options = new DbContextOptionsBuilder<AppDbContext>()
                 .UseInMemoryDatabase(databaseName: "Add_writes_to_select_cell_database")
                 .Options;

             var appDbContext = new AppDbContext(options);

             var loggerRepository = new LoggerRepository(appDbContext);
             var loggerService = new LoggerService(loggerRepository);
         
             var cellRepository = new CellRepository(appDbContext);
             var cellService = new CellService(cellRepository);
         
             var cellController = new PCellsController(
                 cellService,
                 _mapper,
                 loggerService);

             appDbContext.Cells.Add(new Cell
             {
                 Id = 1,
                 Beds = 2,
                 IdPrison = 1,
                 IdCellType = 1,
                 Prison = new Prison(),
                 CellType = new CellType(),
                 Prisoner = new List<Prisoner>()
             });
             appDbContext.SaveChanges();

             var createdCell = appDbContext.Cells.FirstOrDefault(x => x.Id == 1);
             Assert.IsTrue(appDbContext.Cells.Any(),"there is no cell");
             Assert.IsNotNull(createdCell, "cell was not created properly");
             Assert.IsNotNull(cellController.SelectedCell(1),"selected cell returns null");
          //   Assert.IsNotNull(cellController.SelectedCell(1).Value,"Selected cell ok results value is null"); //rip c#7, gonna be fixed in c#8
          //   Assert.AreEqual(_mapper.Map<CellVM>(createdCell),cellController.SelectedCell(1).Value,"Selected Cell returns wrong value");// rip c#7, gonna be fixed in c#8


         }
        [Test]
        public void IsDeletingSelectedCell()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_Delete_Cell_database")
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
            var loggerRepository = new LoggerRepository(appDbContext);
            var loggerService = new LoggerService(loggerRepository);
        
            var cellRepository = new CellRepository(appDbContext);
            var cellService = new CellService(cellRepository);
        
            var cellController = new PCellsController(
                cellService,
                _mapper,
                loggerService)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } }
            };

            appDbContext.Cells.Add(new Cell
            {
                Id = 1,
                Beds = 2,
                IdPrison = 1,
                IdCellType = 1,
                Prison = new Prison(),
                CellType = new CellType(),
                Prisoner = new List<Prisoner>()
            });
            appDbContext.SaveChanges();


            Assert.AreEqual(appDbContext.Cells.Count(),1);
            cellController.DeleteCell(1);
            Assert.AreEqual(appDbContext.Cells.Count(),0);
        }

        [Test]
        public void IsUpdatingSelectedCell()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_Update_Cell_database")
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
            var loggerRepository = new LoggerRepository(appDbContext);
            var loggerService = new LoggerService(loggerRepository);

            var cellRepository = new CellRepository(appDbContext);
            var cellService = new CellService(cellRepository);

            var cellController = new PCellsController(
                cellService,
                _mapper,
                loggerService)
            {
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } }
            };

            appDbContext.CellTypes.Add(new CellType
            {
                Id = 1,
                CellName = "abcd"
            });
            cellController.AddCell(new CellDTO
            {
                Beds = 1,
                IdCellType = 1,
            });

            var cellToUpdate = new CellDTO
            {
                Beds = 5,
                IdCellType = 1
            };
            

            Assert.AreEqual(appDbContext.Cells.Count(), 1);
            cellController.UpdateCell(1,cellToUpdate);
            Assert.AreEqual(appDbContext.Cells.Count(), 1,"Cell has not deleted or added, instead of updating");
            Assert.AreEqual(appDbContext.Cells.FirstOrDefault(x => x.Id == 1).Beds,5,"The cell updated wrong value");
        }

    }
}
