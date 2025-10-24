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
            /*
            _connectionString = "Server=ASUS-TUF\\SQLEXPRESS;Database=SchoolManagementDb;" +
    "User Id=Practice_Admin;Password=12345678;Trust Server Certificate=True;";
            */

            _connectionString = "Server=.\\SQLEXPRESS;Database=SchoolManagementDb;" +
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
            /* [NOTE: Table Mapping / Table Name Mapping
                      - এটি Fluent API এর মাধ্যমে Topic entity কে Topics table এর সাথে ম্যাপ করছে।
                      - এটাও Fluent API — যা entity Topic কে database table "Topics" এর সাথে bind করছে।
               ]
            */
            modelBuilder.Entity<Topic>().ToTable("Topics");

            /* [NOTE: Entity er Property er ei Featuers ta apply er jonno abr new kore migrations generate korte hobe.
               ]
            */
            modelBuilder.Entity<Topic>().Property(x => x.Name).IsRequired().HasMaxLength(20);





            /* ---------------------------------------------------------------- 
            Topic : Table Relationship : Many to Many (Table : Student, Course)            
            ------------------------------------------------------------------- */

            /* [NOTE: Pivot Table 
                      - Table Relationship : Many to Many 
                      - Table : Student, Course
                      - এখানে Fluent API এর .HasKey() মেথড দিয়ে composite primary key define করা হয়েছে।এটি CourseStudent entity-এর জন্য একটি pivot table (many-to-many) relationship তৈরির অংশ।
               ]
            */
            modelBuilder.Entity<CourseStudent>()
                .HasKey((x) => new { x.CourseId, x.StudentId });  // [NOTE: Lambda Method and Anonymouse Object are being used here.]
            
            modelBuilder.Entity<CourseStudent>().ToTable("CourseStudents");
            




            /* -------------------------------------------------------------------------------------- 
            Topic : Fluent API : Table Relationship : One to Many, One to One (Table : Course, Topic)
            ----------------------------------------------------------------------------------------- */
            
            modelBuilder.Entity<Course>()
                .HasMany(x => x.Topics)
                    .WithOne(y => y.Course)
                        .HasForeignKey(z => z.CourseId);





            /* ------------------------------------------------------------------------------------ 
            Topic : Fluent API : Table Relationship : Many to Many (Table : Course, Topic, Student)
            --------------------------------------------------------------------------------------- */

            /* [NOTE: - Table Relationship : Many to Many 
                      - Table : Student, Course
                      - Always Apply Relationship From "Middle Table" through "Fluent API" 
               ]
            */
            modelBuilder.Entity<CourseStudent>()
                .HasOne(x => x.Student)
                    .WithMany(y => y.Courses)
                        .HasForeignKey(z => z.StudentId);

            modelBuilder.Entity<CourseStudent>()
                .HasOne(x => x.Course)
                    .WithMany(y => y.Students)
                        .HasForeignKey(z => z.CourseId);





            /* -------------------------------------------------------------
               Topic : Seeding : Seeding some data by calling private method
            ---------------------------------------------------------------- */
            /* [NOTE : STEPS
                       - Migration 1 : Entity : Teacher 
                       - Migration 2 : Entity : Teacher : Data Seeding
                       - Database Update : Table : Teacher : ef command (so that table can be created on Database with some seeded Data)
               ]
            */
            modelBuilder.Entity<Teacher>().HasData(GetTeachers().ToArray());
        }





        /* --------------------------------------------------------------- 
           Topic : Seeding : Seeding some data by declaring private method
        ------------------------------------------------------------------ */

        private List<Teacher> GetTeachers()
        {
            return new List<Teacher>()
            {
                new Teacher()
                {
                    Id = -1, Name = "Teacher AAA", Username = "taaa", Password = "123456"
                },
                new Teacher()
                {
                     Id = -2, Name = "Teacher BBB", Username = "tbbb", Password = "123456"
                }
            };
        }





        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
    }
}
