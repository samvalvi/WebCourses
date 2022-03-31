using App.Courses;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        [Route("/course/get-all")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await this._mediator.Send(new SelectAll.CourseList());
        }

        [HttpGet]
        [Route("/course/get-by-id/{id:int}")]
        public async Task<ActionResult<Course>> GetCourseById(int id)
        {
            return await this._mediator.Send(new SelectById.CourseById { Id = id });
        }

        [HttpPost]
        [Route("/course/create")]
        public async Task<ActionResult<Unit>> CreateCourse(Insert.InsertCourse data)
        {
            return await this._mediator.Send(data);
        }
        
        [HttpPut]
        [Route("/course/update/{id:int}")]
        public async Task<ActionResult<Unit>> UpdateCourse(int id, Update.UpdateCourse data)
        {
            data.CourseID = id;
            return await this._mediator.Send(data);
        }
        
        [HttpDelete]
        [Route("/course/delete/{id:int}")]
        public async Task<ActionResult<Unit>> DeleteCourse(int id)
        {
            return await this._mediator.Send(new Delete.DeleteCourse { ID = id });
        }
    }
}
