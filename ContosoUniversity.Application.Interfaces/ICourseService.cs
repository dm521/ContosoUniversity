using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Dto;

namespace ContosoUniversity.Application.Interfaces
{
    public interface ICourseService
    {
        Task<CourseDto> GetCourseById(int courseId);
    }
}
