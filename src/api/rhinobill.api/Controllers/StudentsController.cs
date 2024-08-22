using MediatR;
using Microsoft.AspNetCore.Mvc;
using rhinobill.api.Contracts.Requests.Students;
using rhinobill.core.Application.Students.Commands;
using rhinobill.core.Application.Students.Models;
using rhinobill.core.Application.Students.Queries;

namespace rhinobill.api.Controllers
{
    [Route("api/students")]
    public class StudentsController : BaseController
    {
        private readonly IMediator mediator;

        public StudentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<Student[]> GetStudents()
        {
            var query = new GetStudentsQuery();
            var result = await mediator.Send(query);
            return GetResult(result);
        }

        [HttpGet("{id}")]
        public async Task<Student> GetStudent(Guid id)
        {
            var query = new GetStudentQuery(id);
            var result = await mediator.Send(query);
            return GetResult(result);
        }

        [HttpPost]
        public async Task<Student> CreateStudent([FromBody] CreateStudentRequest request)
        {
            var command = new CreateStudentCommand(request.FirstName, request.LastName, request.DateOfBirth, request.Email, request.PhoneNumber);
            var result = await mediator.Send(command);
            return GetResult(result);
        }

        [HttpPut("{id}")]
        public async Task<Student> UpdateStudent([FromRoute]Guid id, [FromBody] CreateStudentRequest request)
        {
            var command = new UpdateStudentCommand(id, request.FirstName, request.LastName, request.DateOfBirth, request.Email, request.PhoneNumber);
            var result = await mediator.Send(command);
            return GetResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<Guid> DeleteStudent(Guid id)
        {
            var command = new DeleteStudentCommand(id);
            var result = await mediator.Send(command);
            return GetResult(result);
        }
    }
}
