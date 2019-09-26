using Microsoft.EntityFrameworkCore;



namespace SportsStore.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<CustomerProfile> CustomerProfiles { get;set; }
        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<Sale> Sales { get; set; }
    }
}
