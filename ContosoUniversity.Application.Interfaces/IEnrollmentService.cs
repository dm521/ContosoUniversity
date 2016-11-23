using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Dto;

namespace ContosoUniversity.Application.Interfaces
{
    public interface IEnrollmentService
    {
        Task<IList<EnrollmentDto>> GetEnrollmentsByStudentId(int studentId);
    }
}
