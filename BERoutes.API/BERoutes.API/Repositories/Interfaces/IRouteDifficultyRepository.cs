using BERoutes.API.Models.Domain;

namespace BERoutes.API.Repositories.Interfaces
{
    public interface IRouteDifficultyRepository
    {
        Task<IEnumerable<RouteDifficulty>> GetAllAsync();
        Task<RouteDifficulty?> GetAsync(Guid id);
        Task<RouteDifficulty> AddAsync(RouteDifficulty routeDifficulty);
        Task<RouteDifficulty> UpdateAsync(Guid id, RouteDifficulty routeDifficulty);
        Task<RouteDifficulty> DeleteAsync(Guid id);
    }
}
