using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public List<Cart> Cart { get; set; }
    }
}
