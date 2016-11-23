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
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository, IUnitOfWork unitOfWork)
        {
            _enrollmentRepository = enrollmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<EnrollmentDto>> GetEnrollmentsByStudentId(int studentId)
        {
            return await _enrollmentRepository.GetEnrollmentsByStudentId(studentId).Select(x=>new EnrollmentDto {
                EnrollmentId = x.EnrollmentId,
                StudentId = studentId,
                CourseId = x.CourseId,
                Grade = x.Grade
            }).ToListAsync();
        }
    }
}
