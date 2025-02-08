using BERoutes.API.Models.Domain;

namespace BERoutes.API.Repositories.Interfaces
{
    public interface IActivityRouteRepository
    {
        Task<IEnumerable<ActivityRoute>> GetAllAsync();
        Task<ActivityRoute?> GetAsync(Guid id);
        Task<ActivityRoute> AddAsync(ActivityRoute activityRoute);
        Task<ActivityRoute> UpdateAsync(Guid id, ActivityRoute activityRoute);
        Task<ActivityRoute> DeleteAsync(Guid id);
    }
}
