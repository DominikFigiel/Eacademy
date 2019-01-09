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
        public DbSet<CourseStudent> CourseStudents { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<StudentRole> StudentRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasAlternateKey(s => s.Username)
                .HasName("AlternateKey_Username");

            modelBuilder.Entity<CourseStudent>()
                .HasKey(cs => new { cs.CourseId, cs.StudentId });

            modelBuilder.Entity<CourseStudent>()
                .HasOne(cs => cs.Course)
                .WithMany(c => c.CourseStudents)
                .HasForeignKey(cs => cs.CourseId);

            modelBuilder.Entity<CourseStudent>()
                .HasOne(cs => cs.Student)
                .WithMany(s => s.CourseStudents)
                .HasForeignKey(cs => cs.StudentId);


            modelBuilder.Entity<StudentRole>()
                .HasKey(sr => new { sr.StudentId, sr.RoleId });

            modelBuilder.Entity<StudentRole>()
                .HasOne(cs => cs.Role)
                .WithMany(c => c.StudentRoles)
                .HasForeignKey(cs => cs.RoleId)
                .IsRequired();   

            modelBuilder.Entity<StudentRole>()
                .HasOne(cs => cs.Student)
                .WithMany(c => c.StudentRoles)
                .HasForeignKey(cs => cs.StudentId)
                .IsRequired();         

        }
    }
}