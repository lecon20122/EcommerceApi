using Ecommerce.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticate _authenticate;

        public AccountController(IAuthenticate authenticate)
        {
            _authenticate = authenticate;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Protected()
        {
            return Ok("Hello World");
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            return await _authenticate.Login(loginRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            return await _authenticate.Register(registerRequest);
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok("Logout");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return Ok("Access Denied");
        }
    }
}
