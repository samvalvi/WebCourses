using FluentValidation;
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
    public class Delete
    {
        public class DeleteCourse : IRequest
        {
            public int ID { get; set; }
        }

        public class ValidationHandler : AbstractValidator<DeleteCourse>
        {
            public ValidationHandler()
            {
                RuleFor(x => x.ID).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<DeleteCourse>
        {
            private readonly AppDbContext _db;

            public Handler(AppDbContext db)
            {
                this._db = db;
            }
            
            public async Task<Unit> Handle(DeleteCourse request, CancellationToken cancellationToken)
            {
                var curso = await this._db.Courses.AsNoTracking().FirstAsync(x => x.CourseID == request.ID);
                if (curso == null)
                {
                    throw new Exception("Course not found");
                }

                this._db.Courses.Remove(curso);
                var result = await this._db.SaveChangesAsync() > 0;
                if (result)
                {
                    return Unit.Value;
                }

                throw new Exception("Error deleting course");
            }
        }
    }
}
