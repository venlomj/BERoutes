using BERoutes.API.Data;
using BERoutes.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BERoutes.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly BERoutesDbContext context;

        public RegionRepository(BERoutesDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await context.Regions.ToListAsync();
        }
    }
}
