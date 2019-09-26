using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class PaymentsCard
    {
        public int ID { get; set; }
        public string CardType { get; set; }
        public string CardHoldersName { get; set; }
        public int CardNumber { get; set; }
        public int CardExpMonth { get; set; }
        public int CardExpYear { get; set; }
    }
}
