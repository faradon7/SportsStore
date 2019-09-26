using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class CustomerProfile
    {
        public int ID { get; set; }
        public int ApplicationUserID { get; set; }
        public List<PaymentsCard> PaymentsCards { get; set; }
        public Location Location { get; set; }
        public int LocationID { get; set; }
        public List<Order> Orders { get; set; }
        public List<FeedBack> Comments { get; set; }
    }
}
