using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Origins.API.Configs;
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
        public static string GenerateToken(string username, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Role, role)
            };

            var key = JwtConfig.Config.KeyObject;
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(JwtConfig.Config.ExpireDays);

            var token = new JwtSecurityToken(
                JwtConfig.Config.Issuer,
                JwtConfig.Config.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public DateTime RegisterTime { get; set; }
    }

    public class UserRole : IdentityRole
    {
        public const string Admin = "Admin";
        public const string User = "User";

        public UserRole() { }
        public UserRole(string roleName) : base(roleName) { }

      
    }
}
