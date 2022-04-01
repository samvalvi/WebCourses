using App.ErrorHandler;
using Domain;
using FluentValidation;
using MediatR;
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
    public class SelectById
    {
        public class CourseById : IRequest<Course> 
        {
            public int Id { get; set; }
        }

        public class ValidationHandler : AbstractValidator<CourseById>
        {
            public ValidationHandler()
            {
                RuleFor(x => x.Id).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<CourseById, Course>
        {
            private readonly AppDbContext _db;
            public Handler(AppDbContext db)
            {
                this._db = db;
            }
            
            public async Task<Course> Handle(CourseById request, CancellationToken cancellationToken)
            {
                var course = await this._db.Courses.FindAsync(request.Id);
                if (course == null)
                {
                    throw new Error(HttpStatusCode.NotFound, new { message = "Course not found" });
                }
                return course;
            }
        }
    }
}
