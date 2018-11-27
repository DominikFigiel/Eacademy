using EacademyApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EacademyApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Modules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        modelBuilder.Entity<Student>()
            .HasAlternateKey(s => s.Username)
            .HasName("AlternateKey_Username");
        }
    }
}
