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

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await context.AddAsync(region);
            await context.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await context.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await context.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await context.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null;
            }
            
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;

            await context.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await context.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (region == null)
            {
                return null;
            }

            context.Regions.Remove(region);
            await context.SaveChangesAsync();
            return region;
        }
    }
}
