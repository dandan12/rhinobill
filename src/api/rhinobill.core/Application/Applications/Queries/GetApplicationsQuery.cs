using rhinobill.core.Application.Applications.Abstractions;
using rhinobill.core.Pipelines.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.core.Application.Applications.Queries
{
    public record GetApplicationsQuery() : IQuery<ApplicationModel[]>;
    public class GetApplicationsQueryHandler : IQueryHandler<GetApplicationsQuery, ApplicationModel[]>
    {
        private readonly IApplicationRepository applicationRepository;

        public GetApplicationsQueryHandler(IApplicationRepository applicationRepository)
        {
            this.applicationRepository = applicationRepository;
        }

        public async Task<Result<ApplicationModel[]>> Handle(GetApplicationsQuery request, CancellationToken cancellationToken)
        {
            return await applicationRepository.GetAll();
        }
    }
}
