using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Courses
{
    public class Update
    {
        public class UpdateCourse : IRequest
        {
            [Key]
            public int CourseID { get; set; }
            public string Title { get; set; }
            [StringLength(50, ErrorMessage = "Description must be less than 50 characters")]
            public string Description { get; set; }
            public DateTime? StartDate { get; set; }
        }

        public class Handler : IRequestHandler<UpdateCourse>
        {
            public readonly AppDbContext _db;
            public Handler(AppDbContext db)
            {
                this._db = db;
            }

            public async Task<Unit> Handle(UpdateCourse request, CancellationToken cancellationToken)
            {
                var course = await _db.Courses.AsNoTracking().FirstAsync(c => c.CourseID == request.CourseID);
                if (course == null)
                {
                    throw new Exception("Course not found");
                }

                course.Title = request.Title ?? course.Title;
                course.Description = request.Description ?? course.Description;
                course.StartDate = request.StartDate ?? course.StartDate;

                _db.Courses.Update(course);
                this._db.Entry(course).State = EntityState.Modified;
                var result = await this._db.SaveChangesAsync() > 0;
                if (result)
                {
                    return Unit.Value;
                }

                throw new Exception("Problem updating course");
            }
        }
    }
}
