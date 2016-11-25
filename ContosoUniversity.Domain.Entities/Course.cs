using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Domain.Entities
{
    public class Course
    {
        [Display(Name ="Number")]
        public int CourseId { get; set; }

        [StringLength(50,MinimumLength =3)]
        public string Title { get; set; }

        [Range(0,5)]
        public int Credits { get; set; }

        public int DepartmentID { get; set; }

        public Department Department { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

        public ICollection<CourseAssignment> Assigements { get; set; }
    }
}
