using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Origins.API.Auth
{
    public class AuthService
    {
        private JwtConfig jwtConfig;

        public AuthService(IOptions<JwtConfig> jwtConfig)
        {
            this.jwtConfig = jwtConfig.Value;
        }

        public string GenerateToken(string username, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Role, role)
            };

            var key = jwtConfig.KeyObject;
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(jwtConfig.ExpireDays);

            var token = new JwtSecurityToken(
                jwtConfig.Issuer,
                jwtConfig.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
