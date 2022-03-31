using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Compose primary key
            modelBuilder.Entity<CourseInstructor>().HasKey(ci => new { ci.InstructorID, ci.CourseID });
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<CourseInstructor> CourseInstructors { get; set; }
    }
}
