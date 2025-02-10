using BERoutes.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BERoutes.API.Data
{
    public class BERoutesDbContext : DbContext
    {
        public BERoutesDbContext(DbContextOptions<BERoutesDbContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.Role)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.User)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(x => x.UserId);
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<ActivityRoute> ActivityRoutes { get; set; }
        public DbSet<RouteDifficulty> RouteDifficulties { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
