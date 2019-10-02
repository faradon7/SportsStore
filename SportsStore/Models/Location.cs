using System.ComponentModel.DataAnnotations;

namespace SportsStore.Models
{
    public class Location 
    {
        public int ID { get; set; } 

        [Required(ErrorMessage = "Please enter the first address line")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Please enter a city name")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter a region name")]
        public string Region { get; set; }
        public string Zip { get; set; }

        [Required(ErrorMessage = "Please enter a country name")]
        public string Country { get; set; }
    }
}
