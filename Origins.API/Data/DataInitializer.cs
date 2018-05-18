using Microsoft.AspNetCore.Identity;
using Origins.API.Models;
using Origins.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Origins.API.Data
{
    public class DataInitializer
    {

        private readonly ApplicationContext context;
        private readonly RoleManager<UserRole> roleManager;

        public DataInitializer(ApplicationContext context, RoleManager<UserRole> roleManager)
        {
            this.context = context;
            this.roleManager = roleManager;
        }

        private async Task InitializeRole(string roleName)
        {
            if (!(await roleManager.RoleExistsAsync(roleName)))
            {
                await roleManager.CreateAsync(new UserRole(roleName));
            }
        }

        public void Initialize()
        {
            // ensure database created
            context.Database.EnsureCreated();

            // create admin roles if not already exists
            foreach (var role in UserRole.PredefinedRoles)
            {
                InitializeRole(role).Wait();
            }
            
        }
    }
}
