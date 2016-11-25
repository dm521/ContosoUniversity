using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Domain.Entities;
using ContosoUniversity.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace ContosoUniversity.UnitOfWork.Persistence
{
    public class EntityContext : DbContext, IDbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Instructor>().ToTable("Instructor");
            modelBuilder.Entity<OfficeAssignment>().ToTable("OfficeAssignment");
            modelBuilder.Entity<CourseAssignment>().ToTable("CourseAssignment");

            modelBuilder.Entity<CourseAssignment>().HasKey(c=>new
            {
                c.CourseID,
                c.InstructorID
            }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
