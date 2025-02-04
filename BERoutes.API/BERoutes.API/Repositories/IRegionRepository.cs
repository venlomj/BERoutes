using BERoutes.API.Models.Domain;

namespace BERoutes.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
    }
}
