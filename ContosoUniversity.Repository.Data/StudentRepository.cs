using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Domain.Entities;
using ContosoUniversity.Repository.Interfaces;
using ContosoUniversity.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Repository.Data
{
    public class StudentRepository : IStudentRepository
    {

        private readonly IQueryable<Student> _students;

        public StudentRepository(IDbContext dbContext)
        {
            _students = dbContext.Set<Student>();
        }

        public IQueryable<Student> GetStudents()
        {
            return  _students.OrderBy(x=>x.FirstName);
        }

        public IQueryable<Student> GetStudentById(int Id)
        {
            return _students.Include(s => s.Enrollments).ThenInclude(e => e.Course).Where(x => x.ID == Id);
        }
    }
}
