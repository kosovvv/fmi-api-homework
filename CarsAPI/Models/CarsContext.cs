using Microsoft.EntityFrameworkCore;

namespace CarsAPI.Models
{
    public class CarsContext(DbContextOptions<CarsContext> options) : DbContext(options)
    {
        public DbSet<Car> Cars { get; set; }

        public DbSet<Garage> Garages { get; set; }

        public DbSet<Maintenance> Maintenances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
