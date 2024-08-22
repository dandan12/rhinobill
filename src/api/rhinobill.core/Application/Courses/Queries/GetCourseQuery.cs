using rhinobill.core.Application.Courses.Abstractions;
using rhinobill.core.Application.Courses.Models;
using rhinobill.core.Pipelines.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.core.Application.Courses.Queries
{
    public record GetCourseQuery(Guid Id) : IQuery<Course>;
    public class GetCourseQueryHandler : IQueryHandler<GetCourseQuery, Course>
    {
        private readonly ICourseRepository courseRepository;

        public GetCourseQueryHandler(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public async Task<Result<Course>> Handle(GetCourseQuery request, CancellationToken cancellationToken)
        {
            var course = await courseRepository.Get(request.Id);
            if (course is null) return ErrorResult.NotFound;

            return course;
        }
    }
}
