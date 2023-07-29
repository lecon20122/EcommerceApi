using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [DefaultValue(1)]
        public int Quantity { get; set; }
        [ForeignKey("CategoryId")]
        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<ProductImage>? ProductImage { get; set; }
        public List<Cart>? Cart { get; set; }
        public List<Order>? Orders { get; set; }
        public List<OrderProduct>? OrderProducts { get; set; }
    }
}
