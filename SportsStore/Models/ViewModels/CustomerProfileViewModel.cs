using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsStore.Models.ViewModels
{
    public class CustomerProfileViewModel
    {
        [NotMapped]
        public List<PaymentsCard> PaymentsCards { get; set; }
        public Location Location { get; set; }

        [NotMapped]
        public int LocationID { get; set; }
        public List<Order> Orders { get; set; }
        public List<FeedBack> Comments { get; set; }
    }
}
