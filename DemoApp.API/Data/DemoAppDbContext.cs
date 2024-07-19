using DemoApp.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.API.Data
{
    public class DemoAppDbContext: DbContext
    {
        public DemoAppDbContext(DbContextOptions<DemoAppDbContext> dbContextOptions):base(dbContextOptions) { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Teacher> Teachers{ get; set; }
    }
}
