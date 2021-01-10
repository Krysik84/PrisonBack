using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrisonBack.Domain.Models;


namespace PrisonBack.Persistence.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<Cell> Cells { get; set; }
        public virtual DbSet<CellType> CellTypes { get; set; }
        public virtual DbSet<Prison> Prisons { get; set; }
        public virtual DbSet<Prisoner> Prisoners { get; set; }
        public virtual DbSet<Reason> Reasons { get; set; }
        public virtual DbSet<Punishment> Punishments { get; set; }
        public virtual DbSet<Pass> Passes { get; set; }
        public virtual DbSet<Isolation> Isolations { get; set; }
        public virtual DbSet<Logger> Loggers { get; set; }
        public virtual DbSet<InviteCode> InviteCodes { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);



        


        }

    }
}
