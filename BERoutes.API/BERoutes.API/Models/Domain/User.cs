using System.ComponentModel.DataAnnotations.Schema;

namespace BERoutes.API.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }


        // Navigation property
        public List<UserRole> UserRoles { get; set; }

        [NotMapped]
        public List<string> Roles { get; set; }

    }
}
