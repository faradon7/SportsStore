using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class EFProfileRepository : IProfileRepository
    {
        private ApplicationDbContext context;

        public EFProfileRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<CustomerProfile> Profiles => context.CustomerProfiles.Include(p => p.Location);

        public void SaveProfile(CustomerProfile profile)
        {
            var dbEntryProfile = context.CustomerProfiles.Include(p => p.Location).FirstOrDefault(
                p => p.ApplicationUserID == profile.ApplicationUserID);

            if (dbEntryProfile == null)
            {
                context.CustomerProfiles.Add(profile);
            }
            else
            {
                profile.ID = dbEntryProfile.ID;
                profile.Location.ID = dbEntryProfile.Location.ID;
                context.Entry(dbEntryProfile).CurrentValues.SetValues(profile);

                context.Entry(dbEntryProfile.Location).CurrentValues.SetValues(profile.Location);
            }
                context.SaveChanges();
        }

        public CustomerProfile DeleteProfile(int userID)
        {
            throw new NotImplementedException();
        }

    }
}
