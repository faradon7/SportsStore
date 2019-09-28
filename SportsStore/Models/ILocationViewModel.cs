using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public interface ILocationViewModel
    {
        string Line1 { get; set; }
        string Line2 { get; set; }
        string Line3 { get; set; }
        string City { get; set; }
        string Region { get; set; }
        string Zip { get; set; }
        string Country { get; set; }
    }
}
