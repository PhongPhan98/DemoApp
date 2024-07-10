using DemoApp.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.API.Data
{
    public class DemoAppDbContext: DbContext
    {
        public DemoAppDbContext(DbContextOptions dbContextOptions):base(dbContextOptions) { }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
