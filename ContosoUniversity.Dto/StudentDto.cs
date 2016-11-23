using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ContosoUniversity.Dto
{
    //[MetadataType(typeof(StudentValidaate)]
    public class StudentDto
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public IEnumerable<EnrollmentDto> Enrollments { get; set; }

    }
}
