using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Domain.Entities;
using ContosoUniversity.Repository.Interfaces;
using ContosoUniversity.UnitOfWork.Interfaces;

namespace ContosoUniversity.Repository.Data
{
    public class CourseRepository : ICourseRepository
    {
        private readonly IQueryable<Course> _course;

        public CourseRepository(IDbContext dbContext)
        {
            _course = dbContext.Set<Course>();
        }

        public IQueryable<Course> GetCoursesById(int courseId)
        {
            return _course.Where(x => x.CourseId == courseId);
        }
    }
}
