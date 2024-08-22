using MediatR;
using Microsoft.AspNetCore.Mvc;
using rhinobill.api.Contracts.Requests.Courses;
using rhinobill.core.Application.Courses.Commands;
using rhinobill.core.Application.Courses.Models;
using rhinobill.core.Application.Courses.Queries;

namespace rhinobill.api.Controllers
{
    [Route("api/courses")]
    public class CoursesController : BaseController
    {
        private readonly IMediator mediator;

        public CoursesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<Course[]> GetCourses()
        {
            var query = new GetCoursesQuery();
            var result = await mediator.Send(query);
            return GetResult(result);
        }

        [HttpGet("{id}")]
        public async Task<Course> GetCourse(Guid id)
        {
            var query = new GetCourseQuery(id);
            var result = await mediator.Send(query);
            return GetResult(result);
        }

        [HttpPost]
        public async Task<Course> CreateCourse([FromBody] CreateCourseRequest request)
        {
            var command = new CreateCourseCommand(request.Code, request.Title, request.Credits);
            var result = await mediator.Send(command);
            return GetResult(result);
        }

        [HttpPut("{id}")]
        public async Task<Course> UpdateCourse([FromRoute]Guid id, [FromBody] CreateCourseRequest request)
        {
            var command = new UpdateCourseCommand(id, request.Code, request.Title, request.Credits);
            var result = await mediator.Send(command);
            return GetResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<Guid> DeleteCourse([FromRoute]Guid id)
        {
            var command = new DeleteCourseCommand(id);
            var result = await mediator.Send(command);
            return GetResult(result);
        }
    }
}
