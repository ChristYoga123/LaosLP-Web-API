using LAOS_LP_API.Models;
using Microsoft.EntityFrameworkCore;

namespace LAOS_LP_API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> categories { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<Lesson> lessons { get; set; }

    }
}
