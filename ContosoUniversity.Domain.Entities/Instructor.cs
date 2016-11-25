using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Domain.Entities
{
    public class Instructor
    {
        public int ID { get; set; }

        [Required]
        [Display(Name ="Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [Display(Name ="First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        [Display(Name ="Hire Date")]
        public DateTime HireDate { get; set; }

        public ICollection<CourseAssignment> Courses { get; set; }
        public OfficeAssignment OfficeAssignment { get; set; }

    }
}
