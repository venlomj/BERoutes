
using BERoutes.API.Models.Domain;

namespace BERoutes.API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}
