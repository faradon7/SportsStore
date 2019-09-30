using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class CustomerProfile
    {
        public int ID { get; set; }
        public string ApplicationUserID { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [StringLength(15)]
        public string PhoneNumber { get; set; }
        public List<PaymentsCard> PaymentsCards { get; set; }
        public Location Location { get; set; }

        //public int LocationID { get; set; }
        public List<Order> Orders { get; set; }
        public List<FeedBack> Comments { get; set; }
    }
}
