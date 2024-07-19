using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.API.Data
{
    public class DemoAppAuthDbContext: IdentityDbContext
    {
        public DemoAppAuthDbContext(DbContextOptions<DemoAppAuthDbContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "06a0dcbb-8457-43b6-bb36-9589c713545d";
            var writerRoleId = "51f6535e-b49e-4f81-9370-76bbd0386f2c";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "READER"
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "WRITER"
                }
            };


            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
    
}
