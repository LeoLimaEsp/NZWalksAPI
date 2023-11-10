using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Data
{
    public class NZWalksDbContext : DbContext 
    {
        public NZWalksDbContext(DbContextOptions<NZWalksAuthDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //  Seed data for Difficulties: Easy, Medium, Hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("356a7dd2-3aa8-4a8e-90e0-78e787fd05e7"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("e03d4053-c7e5-4327-897a-dc390e419a58"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("cfa401e9-493c-435d-92e7-ef1319ff3306"),
                    Name = "Hard"
                }
            };

            //  Seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //  Seed data for Regions: Auckland

            var regions = new List<Region>()
            {
                new Region
                {
                    Id = Guid.Parse("2da9cfbb-5753-4abe-81e3-9e576e3167a3"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "https://Demo.jpg"
                }
            };

            //  Seed Region to the database
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
