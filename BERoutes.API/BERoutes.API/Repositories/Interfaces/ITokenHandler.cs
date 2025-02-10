using BERoutes.API.Models.Domain;

namespace BERoutes.API.Repositories.Interfaces
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
