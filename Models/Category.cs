using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Remote(action: "IsCategoryNameExists", controller: "Categories")]
        public string Name { get; set; }
    }
}