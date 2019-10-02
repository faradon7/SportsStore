using System.Linq;

namespace SportsStore.Models
{
    public interface IProfileRepository
    {
        IQueryable<CustomerProfile> Profiles { get; }
        void SaveProfile(CustomerProfile profile);
        CustomerProfile DeleteProfile(int userID);
    }
}
