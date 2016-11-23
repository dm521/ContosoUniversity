using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Repository.Interfaces;
using ContosoUniversity.Domain.Entities;
using ContosoUniversity.UnitOfWork.Interfaces;


namespace ContosoUniversity.Repository.Data
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly IQueryable<Enrollment> _enrollments;

        public EnrollmentRepository(IDbContext dbContext)
        {
            _enrollments = dbContext.Set<Enrollment>();
        }

        public IQueryable<Enrollment> GetEnrollmentsByStudentId(int studentId)
        {
            var query = _enrollments.Where(x => x.StudentId == studentId);
            return query;
        }
    }
}
