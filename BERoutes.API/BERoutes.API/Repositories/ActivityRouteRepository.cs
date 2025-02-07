using BERoutes.API.Data;
using BERoutes.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BERoutes.API.Repositories
{
    public class ActivityRouteRepository : IActivityRouteRepository
    {
        private readonly BERoutesDbContext context;

        public ActivityRouteRepository(BERoutesDbContext context)
        {
            this.context = context;
        }

        public async Task<ActivityRoute> AddAsync(ActivityRoute activityRoute)
        {
            activityRoute.Id = Guid.NewGuid();
            await context.ActivityRoutes.AddAsync(activityRoute);
            await context.SaveChangesAsync();
            
            return activityRoute;
        }

        public async Task<IEnumerable<ActivityRoute>> GetAllAsync()
        {
            return await context.ActivityRoutes
                .Include(r => r.Region)
                .Include(d => d.RouteDifficulty)
                .ToListAsync();
        }

        public async Task<ActivityRoute?> GetAsync(Guid id)
        {
            return await context.ActivityRoutes.
                Include(r => r.Region)
                .Include(d => d.RouteDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ActivityRoute> UpdateAsync(Guid id, ActivityRoute activityRoute)
        {
            var existingActivityRoute = await context.ActivityRoutes.FindAsync(id);

            if (existingActivityRoute != null)
            {
                existingActivityRoute.Length = activityRoute.Length;
                existingActivityRoute.Name = activityRoute.Name;
                existingActivityRoute.RouteDifficultyId = activityRoute.RouteDifficultyId;
                existingActivityRoute.RegionId = activityRoute.RegionId;

                await context.SaveChangesAsync();
                return activityRoute;
            }

            return null;
        }

        public async Task<ActivityRoute> DeleteAsync(Guid id)
        {
            var existingActivityRoute = await context.ActivityRoutes.FindAsync(id);

            if (existingActivityRoute == null)
            {
                return null;
            }

            context.ActivityRoutes.Remove(existingActivityRoute);
            await context.SaveChangesAsync();
            return existingActivityRoute;
        }
    }
}
