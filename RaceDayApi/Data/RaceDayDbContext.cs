using Microsoft.EntityFrameworkCore;
using RaceDayApi.Models;

namespace RaceDayApi.Data
{
    public class RaceDayDbContext : DbContext
    {
        public RaceDayDbContext(DbContextOptions<RaceDayDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Enrolment> Enrolments { get; set; }
        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    FullName = "Kabuya Organiser",
                    Email = "organiser@raceday.co.za",
                    PasswordHash = Services.PasswordHasher.HashPassword("AdminPass123!"),
                    Role = "Organiser"
                },
                new User
                {
                    UserId = 2,
                    FullName = "Kabuya Participant",
                    Email = "participant@raceday.co.za",
                    PasswordHash = Services.PasswordHasher.HashPassword("UserPass123!"),
                    Role = "Participant"
                }
            );
        }
    }
}