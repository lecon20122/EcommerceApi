using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Authentication
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "please fill up the email field")]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "please fill up the password field")]
        public string Password { get; set; }
    }
}
