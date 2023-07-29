using Ecommerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // i could delete it and use the indirect way (the easier) to create the many to many relationship
            // but i want to use the direct way to create the many to many relationship for learning purpose
            // when i created the migration there's nothing changed between the two ways

            builder.Entity<Product>()
                .HasMany(p => p.Orders)
                .WithMany(c => c.Products)
                .UsingEntity<OrderProduct>
                (
                    j => j
                        .HasOne(pt => pt.Order)
                        .WithMany(t => t.OrderProducts)
                        .HasForeignKey(pt => pt.OrderId),
                    j => j
                        .HasOne(pt => pt.Product)
                        .WithMany(p => p.OrderProducts)
                        .HasForeignKey(pt => pt.ProductId),

                    j =>
                    {
                        j.HasKey(t => new { t.OrderId, t.ProductId });
                        j.Property(t => t.Price).HasPrecision(18, 2);
                        j.Property(t => t.Quantity).HasColumnType("int");
                        j.Property(t => t.Status).HasColumnType("nvarchar(max)");
                    }
                );
        }
    }
}
