using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Dto
{
    public class CourseDto
    {
        public int CourseId { get; set; }

        public string Title { get; set; }

        public int Credits { get; set; }

        public ICollection<EnrollmentDto> Enrollments { get; set; }
    }
}
