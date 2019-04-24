using Golden_CRM.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Golden_CRM.Models
{
    public class RoleInitializer
    {
        private static readonly List<IdentityRole> Roles = new List<IdentityRole>()
        {
            new IdentityRole{Name = ApplicationRoles.Member, NormalizedName = ApplicationRoles.Member.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString()},
            new IdentityRole{Name = ApplicationRoles.Admin, NormalizedName = ApplicationRoles.Admin.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString() }
        };

        /// <summary>
        /// Checking database for roles
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void SeedData(IServiceProvider serviceProvider)
        {

            // looking at our database
            using (var dbContext = new UserDbContext(serviceProvider.GetRequiredService<DbContextOptions<UserDbContext>>()))
            {
                dbContext.Database.EnsureCreated();
                AddRoles(dbContext);
            }
        }
        private static void AddRoles(UserDbContext context)
        {
            if (context.Roles.Any()) return;

            foreach (var role in Roles)
            {
                context.Roles.Add(role);
                context.SaveChanges();
            }
        }
    }
}
