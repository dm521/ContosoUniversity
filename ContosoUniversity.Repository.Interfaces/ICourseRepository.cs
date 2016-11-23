using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Domain.Entities;

namespace ContosoUniversity.Repository.Interfaces
{
    public interface ICourseRepository
    {
        IQueryable<Course> GetCoursesById(int courseId);
    }
}
