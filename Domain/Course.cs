using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }
        [Required(ErrorMessage = "Course Name is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(50, ErrorMessage = "Description must be less than 50 characters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }
        public byte[] CoverPicture { get; set; }
        public Price CurrentPrice { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Instructor> InstructorNav { get; set; }
    }
}
