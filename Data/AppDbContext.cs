using Microsoft.EntityFrameworkCore;
using PropertyManagementApp.Models;

namespace PropertyManagementApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Properties> Properties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed default properties
            modelBuilder.Entity<Properties>().HasData(
                new Properties
                {
                    Id = 1,
                    Name = "Sunshine Apartments",
                    Location = "Dubai Marina",
                    Price = 500000
                },
                new Properties
                {
                    Id = 2,
                    Name = "Palm Villas",
                    Location = "Palm Jumeirah",
                    Price = 1200000
                },
                new Properties
                {
                    Id = 3,
                    Name = "Green Park Residence",
                    Location = "Business Bay",
                    Price = 750000
                }
            );
        }
    }
}
