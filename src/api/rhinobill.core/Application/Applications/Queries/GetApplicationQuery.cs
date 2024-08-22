using rhinobill.core.Application.Applications.Abstractions;
using rhinobill.core.Pipelines.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.core.Application.Applications.Queries
{
    public record GetApplicationQuery(Guid Id) : IQuery<ApplicationModel>;
    public class GetApplicationQueryHandler(IApplicationRepository applicationRepository) : IQueryHandler<GetApplicationQuery, ApplicationModel>
    {
        public async Task<Result<ApplicationModel>> Handle(GetApplicationQuery request, CancellationToken cancellationToken)
        {
            var application = await applicationRepository.Get(request.Id);
            if (application is null) return ErrorResult.NotFound;

            return application;
        }
    }
}
