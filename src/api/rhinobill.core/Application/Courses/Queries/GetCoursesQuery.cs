using rhinobill.core.Application.Courses.Abstractions;
using rhinobill.core.Application.Courses.Models;
using rhinobill.core.Pipelines.Abstractions;

namespace rhinobill.core.Application.Courses.Queries
{
    public record GetCoursesQuery() : IQuery<Course[]>;
    public class GetCoursesQueryHandler(ICourseRepository courseRepository) : IQueryHandler<GetCoursesQuery, Course[]>
    {
        public async Task<Result<Course[]>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            return await courseRepository.GetAll();
        }
    }
}
