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
        public DateTime RegisterTime { get; set; }    
    }

    public class UserRole : IdentityRole
    {
        public const string Admin = nameof(UserRole.Admin);
        public const string Client = nameof(UserRole.Client);
        public const string Producer = nameof(UserRole.Producer);

        public static IEnumerable<string> PredefinedRoles => new List<string>() { Admin, Client, Producer };

        public UserRole() { }
        public UserRole(string roleName) : base(roleName) { }

      
    }
}
