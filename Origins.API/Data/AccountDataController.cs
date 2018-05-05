using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Origins.API.DataServices;
using Origins.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Origins.API
{
    public class AccountDataController : IAccountDataService
    {
        private readonly SignInManager<UserModel> signInManager;
        private readonly UserManager<UserModel> userManager;
        private readonly RoleManager<UserRole> roleManager;
        private readonly IConfiguration configuration;

        public IQueryable<UserModel> Users => userManager.Users;

        public IQueryable<UserRole> Roles => roleManager.Roles;

        public AccountDataController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager,
            IConfiguration configuration, RoleManager<UserRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.roleManager = roleManager;
        }

        public UserModel GetUser(string username)
        {
            return userManager.Users.SingleOrDefault(x => x.UserName == username);
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var result = await signInManager.PasswordSignInAsync(username, password, false, false);
            return result.Succeeded;
        }

        public async Task<RegisterResult> RegisterAsync(string username, string password, string roleName)
        {
            var user = new UserModel
            {
                UserName = username,
                RegisterTime = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                var registeredUser = userManager.Users.SingleOrDefault(x => x.UserName == username);
                await userManager.AddToRoleAsync(registeredUser, roleName);
                return new RegisterResult()
                {
                    Succeeded = true,
                };
            }
            else
            {
                return new RegisterResult()
                {
                    Succeeded = false,
                    Errors = result.Errors
                };
            }
        }

        public async Task<string> GetRoleAsync(string username)
        {
            var roles = await userManager.GetRolesAsync(GetUser(username));
            return roles.SingleOrDefault();
        }
    }
}
