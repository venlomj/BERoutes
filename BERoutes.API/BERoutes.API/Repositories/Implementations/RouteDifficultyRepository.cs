using BERoutes.API.Data;
using BERoutes.API.Models.Domain;
using BERoutes.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BERoutes.API.Repositories.Implementations
{
    public class RouteDifficultyRepository : IRouteDifficultyRepository
    {
        private readonly BERoutesDbContext context;

        public RouteDifficultyRepository(BERoutesDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<RouteDifficulty> AddAsync(RouteDifficulty routeDifficulty)
        {
            routeDifficulty.Id = Guid.NewGuid();
            await context.RouteDifficulties.AddAsync(routeDifficulty);
            await context.SaveChangesAsync();
            return routeDifficulty;
        }

        public async Task<IEnumerable<RouteDifficulty>> GetAllAsync()
        {
            return await context.RouteDifficulties.ToListAsync();
        }

        public async Task<RouteDifficulty?> GetAsync(Guid id)
        {
            return await context.RouteDifficulties.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<RouteDifficulty> UpdateAsync(Guid id, RouteDifficulty routeDifficulty)
        {
            var existingRouteDifficulty = await context.RouteDifficulties.FindAsync(id);
            
            if (existingRouteDifficulty == null)
            {
                return null;
            }

            existingRouteDifficulty.Code = routeDifficulty.Code;
            await context.SaveChangesAsync();
            return existingRouteDifficulty;
        }

        public async Task<RouteDifficulty> DeleteAsync(Guid id)
        {
            var existingRouteDifficulty = await context.RouteDifficulties.FindAsync(id);

            if (existingRouteDifficulty != null)
            {
                context.RouteDifficulties.Remove(existingRouteDifficulty);
                await context.SaveChangesAsync();
                return existingRouteDifficulty;
            }

            return null;
        }
    }
}
