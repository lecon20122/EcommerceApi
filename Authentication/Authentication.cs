using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Authentication
{
    public class Authentication : IAuthenticate
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public Authentication(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<IActionResult> Login(LoginRequest login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user is null) return new UnauthorizedObjectResult("Invalid Password or Email");

            var result = await _userManager.CheckPasswordAsync(user, login.Password);

            if (!result) return new UnauthorizedObjectResult("Invalid Password or Email");

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var authSignedKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                           issuer: _configuration["JWT:Issuer"],
                           audience: _configuration["JWT:Audience"],
                           claims: claims,
                           expires: DateTime.Now.AddMinutes(30),
                           signingCredentials: new SigningCredentials(authSignedKey, SecurityAlgorithms.HmacSha256)
              );

            return new OkObjectResult(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            }

         );
        }

        public Task<IActionResult> Logout()
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Register(RegisterRequest register)
        {
            var userExists = await _userManager.FindByEmailAsync(register.Email);

            if (userExists != null) return new BadRequestObjectResult("User already exists");

            ApplicationUser user = new ApplicationUser
            {
                Email = register.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = register.Name
            };

            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(result.Errors.First());

            return new OkObjectResult(new Response
            {
                Message = "User created successfully",
                Status = "Success"
            });
        }
    }
}
