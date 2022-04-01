using App.ErrorHandler;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Courses
{
    public class Update
    {
        public class UpdateCourse : IRequest
        {
            public int CourseID { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime? StartDate { get; set; }
        }

        public class ValidationHandler : AbstractValidator<UpdateCourse>
        {
            public ValidationHandler()
            {
                RuleFor(x => x.CourseID).NotEmpty();
                RuleFor(x => x.Title).NotEmpty().MaximumLength(50);
                RuleFor(x => x.Description).NotEmpty().MaximumLength(50);
                RuleFor(x => x.StartDate).NotEmpty();
            }
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
                var course = await _db.Courses.FindAsync(request.CourseID);
                if (course == null)
                {
                    throw new Error(HttpStatusCode.NotFound, new { message = "Course not found" });
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

                throw new Error(HttpStatusCode.BadRequest,new { message = "Problem updating course" });
            }
        }
    }
}
