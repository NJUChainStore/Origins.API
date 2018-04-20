using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Origins.Models
{
    public class UserModel : IdentityUser
    {
    }

    public class UserRole : IdentityRole
    {
        public const string Admin = "Admin";
        public const string User = "User";

        public UserRole() { }
        public UserRole(string roleName) : base(roleName) { }

      
    }
}
