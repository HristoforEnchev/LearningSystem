namespace LearningSystem.Services.Admin.Implementation
{
    using LearningSystem.Data;
    using LearningSystem.Services.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AdminUserService : IAdminUserService
    {
        private readonly LearningSystemDbContext db;

        public AdminUserService(LearningSystemDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<AdminUserListingServiceModel> All()
        {
            return this.db
                .Users
                .Select(u => new AdminUserListingServiceModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    Username = u.UserName
                })
                .ToList();
        }
    }
}
