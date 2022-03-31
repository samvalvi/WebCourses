using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Courses
{
    public class SelectAll
    {
        public class CourseList : IRequest<List<Course>> { }
        public class Handler : IRequestHandler<CourseList, List<Course>>
        {
            private readonly AppDbContext _db;
            public Handler(AppDbContext db)
            {
                this._db = db;
            }
            
            public async Task<List<Course>> Handle(CourseList request, CancellationToken cancellationToken)
            {
                var courses = await this._db.Courses.ToListAsync();
                return courses;
            }
        }
    }
}
