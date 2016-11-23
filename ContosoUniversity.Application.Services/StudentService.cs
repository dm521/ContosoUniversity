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

    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateStudent(string FirstName, string LastName, DateTime enrollmentDate)
        {
            var student = new Student { FirstName = FirstName, LastName = LastName, EnrollmentDate = enrollmentDate };
            _unitOfWork.Add(student);

            if (await _unitOfWork.CommitAsync())
            {
                
                return student.ID;
            }
            return -1;
        }

        public async Task<bool> EditStudent(int Id, string firstName, string lastName, DateTime enrollmentDate)
        {
            var student = await _studentRepository.GetStudentById(Id).FirstOrDefaultAsync();

            student.FirstName = firstName.Trim();
            student.LastName = lastName.Trim();

            _unitOfWork.Dirty(student);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<Student> GetStudentById(int Id)
        {
            /*return await _studentRepository.GetStudentsById(Id).Select(x => new StudentDto {
                ID = Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                EnrollmentDate = x.EnrollmentDate,

            }).FirstOrDefaultAsync();*/
            return await _studentRepository.GetStudentById(Id).FirstOrDefaultAsync();
        }

        public async Task<IList<StudentDto>> GetStudents()
        {
            return await _studentRepository.GetStudents().OrderBy(x => x.ID).Select(x => new StudentDto
            {
                ID = x.ID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                EnrollmentDate = x.EnrollmentDate
            }).ToListAsync();
        }
    }
}
