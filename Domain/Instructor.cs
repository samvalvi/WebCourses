using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Instructor
    {
        [Key]
        public int InstructorID { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "A brief description is required")]
        public string Profile { get; set; }
        public byte[] ProfilePic { get; set; }
        public ICollection<Course> CourseNav { get; set; }
    }
}
