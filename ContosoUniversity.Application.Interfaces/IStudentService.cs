using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Dto;
using ContosoUniversity.Domain.Entities;

namespace ContosoUniversity.Application.Interfaces
{
    public interface IStudentService
    {
        Task<IList<StudentDto>> GetStudents();

        Task<Student> GetStudentById(int Id);

        Task<bool> EditStudent(int Id,string firstName, string lastName,DateTime enrollmentDate);

        Task<int> CreateStudent(string FirstName, string LastName, DateTime enrollmentDate);
    }
}
