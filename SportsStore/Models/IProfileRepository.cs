using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public interface IProfileRepository
    {
        IQueryable<CustomerProfile> Profiles { get; }
        void SaveProfile(CustomerProfile profile);
        CustomerProfile DeleteProfile(int userID);
    }
}
