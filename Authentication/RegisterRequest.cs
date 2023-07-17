using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Authentication
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "fill up the Name field")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "fill up the Email field")]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "fill up the password field")]
        public string Password { get; set; }
    }
}
