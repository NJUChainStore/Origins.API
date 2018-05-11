using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origins.API.Auth
{
    public class JwtConfig
    {

        public string Issuer { get; set; }
        public string Key { get; set; }
        public SymmetricSecurityKey KeyObject => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
        public int ExpireDays { get; set; }




    }
}
