using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.Domain.Entities;

namespace ContosoUniversity.Dto
{
    public class EnrollmentDto
    {
        public int CourseId { get; set; }

        public int EnrollmentId { get; set; }

        public int StudentId { get; set; }

        public Grade? Grade { get; set; }

        public CourseDto Course { get; set; }

        public StudentDto Student { get; set; }
    }
}
