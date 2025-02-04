using BERoutes.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BERoutes.API.Data
{
    public class BERoutesDbContext : DbContext
    {
        public BERoutesDbContext(DbContextOptions<BERoutesDbContext> options): base(options)
        {
            
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<ActivityRoute> ActivityRoutes { get; set; }
        public DbSet<RouteDifficulty> RouteDifficulties { get; set; }
    }
}
