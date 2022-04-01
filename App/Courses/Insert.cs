using Domain;
using FluentValidation;
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

                this._db.Courses.Add(course);
                var success = await this._db.SaveChangesAsync() > 0;
                if(success)
                {
                    return Unit.Value;
                }
                    
                throw new Exception("Problem saving changes");
                    
            }
        }

    }
}
