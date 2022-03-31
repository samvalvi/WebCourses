using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Price
    {
        [Key]
        public int PriceID { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public decimal CurrentPrice { get; set; }
        [Required(ErrorMessage = "Promotion price is required")]
        public int Promotion { get; set; }
        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public Course Course { get; set; }
    }
}
