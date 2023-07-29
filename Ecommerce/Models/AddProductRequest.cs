using Ecommerce.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Ecommerce.Models
{
    public class AddProductRequest
    {
        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string Name { get; set; }
        [AllowNull]
        [StringLength(255, MinimumLength = 3)]
        public string Description { get; set; }
        [DefaultValue(1)]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".webp" })]
        public List<IFormFile> Images { get; set; }
    }
}
