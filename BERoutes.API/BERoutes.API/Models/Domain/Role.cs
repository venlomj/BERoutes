﻿namespace BERoutes.API.Models.Domain
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        // Navigation property
        public List<UserRole> UserRoles { get; set; }
    }
}
