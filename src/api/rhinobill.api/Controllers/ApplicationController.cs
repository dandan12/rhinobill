using MediatR;
using Microsoft.AspNetCore.Mvc;
using rhinobill.api.Contracts.Requests.Applications;
using rhinobill.core.Application.Applications.Commands;
using rhinobill.core.Application.Applications.Models;
using rhinobill.core.Application.Applications.Queries;

namespace rhinobill.api.Controllers
{
    [Route("api/applications")]
    public class ApplicationController : BaseController
    {
        private readonly IMediator mediator;

        public ApplicationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<Application[]> GetAll()
        {
            var query = new GetApplicationsQuery();
            var result = await mediator.Send(query);
            return GetResult(result);
        }

        [HttpGet("{id}")]
        public async Task<Application> Get([FromRoute] Guid id)
        {
            var query = new GetApplicationQuery(id);
            var result = await mediator.Send(query);
            return GetResult(result);
        }

        [HttpPost]
        public async Task<Application> CreateApplication([FromBody] CreateApplicationRequest request)
        {
            var command = new CreateApplicationCommand(request.StudentId, request.CourseId, request.ApplicationDate);
            var result = await mediator.Send(command);
            return GetResult(result);
        }

        [HttpPut("{id}")]
        public async Task<Application> UpdateApplication([FromRoute]Guid id, [FromBody] CreateApplicationRequest request)
        {
            var command = new UpdateApplicationCommand(id, request.StudentId, request.CourseId, request.ApplicationDate);
            var result = await mediator.Send(command);
            return GetResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<Guid> DeleteApplication([FromRoute]Guid id)
        {
            var command = new DeleteApplicationCommand(id);
            var result = await mediator.Send(command);
            return GetResult(result);
        }
    }
}
