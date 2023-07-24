namespace Ecommerce.Models
{
    public class Order
    {
        public int Id { get; set; }
        public decimal? Total { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? Shipping { get; set; }
        public string? Status { get; set; }
        public ApplicationUser? User { get; set; }
        public int UserId { get; set; }
        public List<Product>? Products { get; set; }
        public List<OrderProduct>? OrderProducts { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
