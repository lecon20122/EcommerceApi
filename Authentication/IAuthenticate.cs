using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Authentication
{
    public interface IAuthenticate
    {
        Task<IActionResult> Login(LoginRequest login);
        Task<IActionResult> Register(RegisterRequest register);
        Task<IActionResult> Logout();
    }
}
