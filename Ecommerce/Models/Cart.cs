﻿using System.ComponentModel;

namespace Ecommerce.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [DefaultValue(1)]
        public int Quantity { get; set; }
        public int? UserId { get; set; }
        public Product Product { get; set; }
        public ApplicationUser User { get; set; }
    }
}
