using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Origins.API.ViewModels.Responses
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
