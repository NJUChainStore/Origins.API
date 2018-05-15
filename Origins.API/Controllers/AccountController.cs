using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Origins.API.Auth;
using Origins.API.DataServices;
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
        private readonly IAccountDataService accountService;
        private readonly AuthService authService;

        public AccountController(IAccountDataService accountService, AuthService authService)
        {
            this.accountService = accountService;
            this.authService = authService;
        }

        [HttpGet("Login")]
        [SwaggerOperation]
        [SwaggerResponse(200, type: typeof(LoginResponse), description: "Login successful. Returns token, username, registerTime and role.")]
        [SwaggerResponse(401, type: typeof(ErrorResponse), description: "Credential provided is not valid.")]
		public async Task<IActionResult> Login([FromQuery]string username, [FromQuery] string password)
        {
            var result = await accountService.LoginAsync(username, password);
            if (result)
            {
                var role = await accountService.GetRoleAsync(username);
                return Json(new LoginResponse()
                {
                    Token = authService.GenerateToken(username, role),
                    Role = role,
                });
            }
            else
            {
                return StatusCode(401, new ErrorResponse()
                {
                    Code = "CredentialInvalid",
                    Description = "Credentials provided are invalid."
                });

            }

        }


        [HttpPost("SignUp")]
        [SwaggerOperation]
        [SwaggerResponse(201, type: typeof(LoginResponse), description: "Login successful. Returns token, username, registerTime and role.")]
        [SwaggerResponse(409, type: typeof(ErrorResponse), description: "Username already exists.")]
		public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var username = model.Username;
            var result = await accountService.RegisterAsync(username, model.Password, model.Role);

            if (result.Succeeded)
            {
                var role = await accountService.GetRoleAsync(username);
                return Created($"api/Account/{username}", new LoginResponse()
                {
                    Token = authService.GenerateToken(username, role),
                    Role = role,
                });
            }
            else
            {
                if (result.ContainsDuplicateUsernameError)
                {
                    return StatusCode(StatusCodes.Status409Conflict, new ErrorResponse()
                    {
                        Code = "UsernameConflict",
                        Description = "Username has been token."
                    });
                }
                else
                {
                    return BadRequest(new MultipleErrorResponse()
                    {
                        Code = "RegisterFailed",
                        ErrorDescriptions = result.Errors.Select(x => x.Description)
                    });
                }

            }
        }


    }
}
