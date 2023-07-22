using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
