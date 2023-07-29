using System.ComponentModel;

namespace Ecommerce.Models
{
    public class AddCartItemRequest
    {
        public int ProductId { get; set; }
        [DefaultValue(1)]
        public int Quantity { get; set; }
        public int? UserId { get; set; }
    }
}
