using Microsoft.AspNetCore.Mvc;
using Origins.API.ViewModels;
using Origins.API.ViewModels.Responses;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Origins.API.Controllers
{
    [Produces("application/json")]
    [Route("Account")]
    public class AccountController : Controller
    {
        [HttpGet("Login")]
        [SwaggerOperation]
        [SwaggerResponse(200, type: typeof(LoginResponse), description: "Login successful. Returns token, username, registerTime and role.")]
        [SwaggerResponse(401, type: typeof(ErrorResponse), description: "Credential provided is not valid.")]
        public async Task<IActionResult> Login([FromQuery]string username, [FromQuery] string password)
        {
            return null;
        }

        [HttpPost("SignUp")]
        [SwaggerOperation]
        [SwaggerResponse(201, type: typeof(LoginResponse), description: "Login successful. Returns token, username, registerTime and role.")]
        [SwaggerResponse(409, type: typeof(ErrorResponse), description: "Username already exists.")]
        public async Task<IActionResult> Register([FromQuery]RegisterViewModel model)
        {
            return null;
        }


    }
}
