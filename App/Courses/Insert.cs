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
    public class Insert
    {
        public class InsertCourse : IRequest 
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime StartDate { get; set; }
        }

        public class ValidationHandler : AbstractValidator<InsertCourse>
        {
            public ValidationHandler()
            {
                RuleFor(x => x.Title).NotEmpty().MaximumLength(50);
                RuleFor(x => x.Description).NotEmpty();
            }
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

                var exist = await _db.Courses.AnyAsync(x => x.Title == request.Title);
                if (exist)
                {
                    throw new Error(HttpStatusCode.BadRequest, new { message = "Course already exist" });
                }

                this._db.Courses.Add(course);
                var success = await this._db.SaveChangesAsync() > 0;
                this._db.Entry(course).State = EntityState.Detached;
                if (success)
                {
                    return Unit.Value;
                }
                    
                throw new Error(HttpStatusCode.BadRequest, new { message = "Problem saving changes" });
                    
            }
        }

    }
}
