using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Courses
{
    public class Insert
    {
        public class InsertCourse : IRequest 
        {
            public string Title { get; set; }
            [StringLength(50, ErrorMessage = "Description must be less than 50 characters")]
            public string Description { get; set; }
            [Required(ErrorMessage = "Start Date is required")]
            public DateTime StartDate { get; set; }
        }

        public class Handler : IRequestHandler<InsertCourse>
        {
            private readonly AppDbContext _db;
            public Handler(AppDbContext db)
            {
                this._db = db;
            }
            
            public async Task<Unit> Handle(InsertCourse request, CancellationToken cancellationToken)
            {
                    var course = new Course();
                    course.Title = request.Title;
                    course.Description = request.Description;
                    course.StartDate = request.StartDate;

                    this._db.Courses.Add(course);
                    var success = await this._db.SaveChangesAsync() > 0;
                    if(success)
                    {
                        return Unit.Value;
                    }
                    else
                    {
                        throw new Exception("Problem saving changes");
                    }
            }
        }

    }
}
