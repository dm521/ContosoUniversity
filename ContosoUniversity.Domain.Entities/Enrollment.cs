using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Domain.Entities
{
    public enum Grade
    {
        A,B,C,D,F
    }

    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        public int CourseId { get; set; }

        public int StudentId { get; set; }

        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade? Grade { get; set; }

        public Student Student { get; set; }

        public Course Course { get; set; }


    }
}
