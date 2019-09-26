using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class Sale
    {
        public int ID { get; set; }
        public Product Product { get; set; }
        public int ProductID { get; set; }
        public decimal SalePrice { get; set; }
    }
}
