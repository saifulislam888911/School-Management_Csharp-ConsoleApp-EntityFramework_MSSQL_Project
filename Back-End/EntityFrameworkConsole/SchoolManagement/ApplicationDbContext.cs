using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;

        public ApplicationDbContext()
        {
            _connectionString = "Server=ASUS-TUF\\SQLEXPRESS;Database=SchoolManagementDb;" +
    "User Id=Practice_Admin;Password=12345678;Trust Server Certificate=True;";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Topic>().ToTable("Topics");


            modelBuilder.Entity<CourseStudent>()
                .HasKey((x) => new { x.CourseId, x.StudentId });
            
            modelBuilder.Entity<CourseStudent>().ToTable("CourseStudents");
            

            modelBuilder.Entity<Course>()
                .HasMany(x => x.Topics)
                    .WithOne(y => y.Course)
                        .HasForeignKey(z => z.CourseId);


            modelBuilder.Entity<CourseStudent>()
                .HasOne(x => x.Student)
                    .WithMany(y => y.Courses)
                        .HasForeignKey(z => z.StudentId);

            modelBuilder.Entity<CourseStudent>()
                .HasOne(x => x.Course)
                    .WithMany(y => y.Students)
                        .HasForeignKey(z => z.CourseId);
        }


        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}