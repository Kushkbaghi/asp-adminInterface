using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminInterface.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AdminInterface.Models.Course> Course { get; set; }
        public DbSet<AdminInterface.Models.Job> Job { get; set; }
        public DbSet<AdminInterface.Models.Project> Project { get; set; }
    }
}