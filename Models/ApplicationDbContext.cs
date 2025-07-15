using Microsoft.EntityFrameworkCore;

namespace ResourceBookingSystem.Models
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : DbContext(options)
    {
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationships
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Resource)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.ResourceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Resources
            modelBuilder.Entity<Resource>().HasData(
                new Resource
                {
                    Id = 1,
                    Name = "Meeting Room A",
                    Description = "Large meeting room with projector",
                    Location = "3rd Floor",
                    Capacity = 10,
                    IsAvailable = true
                },
                new Resource
                {
                    Id = 2,
                    Name = "Company Car 1",
                    Description = "Toyota Corolla",
                    Location = "Parking Bay 1",
                    Capacity = 5,
                    IsAvailable = true
                }
            );

            // Seed Bookings
            modelBuilder.Entity<Booking>().HasData(
                new Booking
                {
                    Id = 1,
                    ResourceId = 1,
                    StartTime = new DateTime(2025, 7, 15, 9, 0, 0),
                    EndTime = new DateTime(2025, 7, 15, 10, 0, 0),
                    BookedBy = "Alice",
                    Purpose = "Morning Standup"
                },
                new Booking
                {
                    Id = 2,
                    ResourceId = 2,
                    StartTime = new DateTime(2025, 7, 16, 14, 0, 0),
                    EndTime = new DateTime(2025, 7, 16, 16, 0, 0),
                    BookedBy = "Bob",
                    Purpose = "Client Visit"
                }
            );
        }
    }
}

