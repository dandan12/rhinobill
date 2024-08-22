using rhinobill.core.Application.Courses.Abstractions;
using rhinobill.core.Pipelines.Abstractions;

namespace rhinobill.core.Application.Courses.Commands
{
    public record DeleteCourseCommand(Guid Id) : ICommand<Guid>;
    public class DeleteCourseCommandHandler : ICommandHandler<DeleteCourseCommand, Guid>
    {
        private readonly ICourseRepository courseRepository;

        public DeleteCourseCommandHandler(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public async Task<Result<Guid>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await courseRepository.Get(request.Id);
            if (course is null) return ErrorResult.NotFound;

            await courseRepository.Delete(request.Id);
            return request.Id;
        }
    }
}
