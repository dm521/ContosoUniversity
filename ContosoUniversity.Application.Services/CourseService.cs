using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Application.Interfaces;
using ContosoUniversity.Dto;
using ContosoUniversity.Repository.Interfaces;
using ContosoUniversity.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Domain.Entities;

namespace ContosoUniversity.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(ICourseRepository courseRepository, IUnitOfWork unitOfWork)
        {
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CourseDto> GetCourseById(int courseId)
        {
            return await _courseRepository.GetCoursesById(courseId).Select(x => new CourseDto
            {
                CourseId = courseId,
                Title = x.Title,
                Credits = x.Credits
            }).FirstOrDefaultAsync();
        }
    }
}
