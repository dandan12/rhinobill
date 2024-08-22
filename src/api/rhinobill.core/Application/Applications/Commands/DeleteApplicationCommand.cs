using rhinobill.core.Application.Applications.Abstractions;
using rhinobill.core.Pipelines.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.core.Application.Applications.Commands
{
    public record DeleteApplicationCommand(Guid Id) : ICommand<Guid>;
    public class DeleteApplicationCommandHandler(IApplicationRepository applicationRepository) : ICommandHandler<DeleteApplicationCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(DeleteApplicationCommand request, CancellationToken cancellationToken)
        {
            var application = await applicationRepository.Get(request.Id);
            if (application is null) return ErrorResult.NotFound;

            await applicationRepository.Delete(request.Id);
            return request.Id;
        }
    }
}
