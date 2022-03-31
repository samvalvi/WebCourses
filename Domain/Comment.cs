using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }
        [Required(ErrorMessage = "A username is required")]
        [StringLength(100, ErrorMessage = "Max length is 100 characters")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Field required.")]
        [StringLength(300, ErrorMessage = "Max length is 300 characters.")]
        public string Content { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public Course Course { get; set; }
    }
}
