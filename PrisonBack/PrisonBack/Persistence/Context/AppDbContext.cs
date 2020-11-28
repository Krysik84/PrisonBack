using Microsoft.EntityFrameworkCore;
using PrisonBack.Domain.Models;


namespace PrisonBack.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<Cell> Cells { get; set; }
        public virtual DbSet<CellType> CellTypes { get; set; }
        public virtual DbSet<Prison> Prisons { get; set; }
        public virtual DbSet<Prisoner> Prisoners { get; set; }
        public virtual DbSet<Reason> Reasons { get; set; }
        public virtual DbSet<Punishment> Punishments { get; set; }
        public virtual DbSet<Pass> Passes { get; set; }
        public virtual DbSet<Isolation> Isolations { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            

            builder.Entity<User>().HasData(
                new User { Id = 100, Login = "Bartek", Password = "xxxx", Mail = "blabla" },
                new User { Id = 101, Login = "Bartek1", Password = "xxxxxx", Mail = "blabla" }
                );

           

            builder.Entity<Permission>().HasData(
                new Permission { Id = 1, PermissionName = "Admin"}
            );
            builder.Entity<UserPermission>().HasData(
                new UserPermission { Id = 1, IdPermission = 1, IdUser = 100}
            );


        }

    }
}
