using BERoutes.API.Data;
using BERoutes.API.Models.Domain;
using BERoutes.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BERoutes.API.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly BERoutesDbContext _dbContext;

        public UserRepository(BERoutesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower()
                && x.Password == password);

            if (user == null)
            {
                return null;
            }

            var userRoles = await _dbContext.UserRoles
                .Where(x => x.UserId == user.Id)
                .ToListAsync();

            if (userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach (var userRole in userRoles)
                { 
                    var role = await _dbContext.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
                    if (role != null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }
            }

            user.Password = null;
            return user;
        }
    }
}
