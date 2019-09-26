using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace SportsStore.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage ="Please enter a product name")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage ="Please enter a positive price")]
        public decimal Price { get; set; }

        public string Category { get; set; }

        IEnumerable<FeedBack> FeedBacks { get; set; }
        public int? PopularityRate { get; set; }
    }
}
