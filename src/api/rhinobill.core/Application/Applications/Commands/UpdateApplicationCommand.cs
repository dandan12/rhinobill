using FluentValidation;
using rhinobill.core.Application.Applications.Abstractions;
using rhinobill.core.Application.Courses.Abstractions;
using rhinobill.core.Application.Students.Abstractions;
using rhinobill.core.Pipelines.Abstractions;

namespace rhinobill.core.Application.Applications.Commands
{
    public record UpdateApplicationCommand(Guid Id, Guid StudentId, Guid CourseId, DateTime ApplicationDate) : ICommand<ApplicationModel>;
    public class UpdateApplicationCommandValidator : AbstractValidator<UpdateApplicationCommand>
    {
        public UpdateApplicationCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.CourseId)
                .NotEmpty();

            RuleFor(x => x.StudentId)
                .NotEmpty();

            RuleFor(x => x.ApplicationDate)
                .NotEmpty();
        }
    }

    public class UpdateApplicationCommandHandler : ICommandHandler<UpdateApplicationCommand, ApplicationModel>
    {
        private readonly IApplicationRepository applicationRepository;
        private readonly IStudentRepository studentRepository;
        private readonly ICourseRepository courseRepository;

        public UpdateApplicationCommandHandler(IApplicationRepository applicationRepository,
            IStudentRepository studentRepository,
            ICourseRepository courseRepository)
        {
            this.applicationRepository = applicationRepository;
            this.studentRepository = studentRepository;
            this.courseRepository = courseRepository;
        }

        public async Task<Result<ApplicationModel>> Handle(UpdateApplicationCommand request, CancellationToken cancellationToken)
        {
            var student = await studentRepository.Get(request.StudentId);
            if (student is null) return ErrorResult.NotFound;

            var course = await courseRepository.Get(request.CourseId);
            if (course is null) return ErrorResult.NotFound;

            var application = await applicationRepository.Get(request.Id);
            if (application is null) return ErrorResult.NotFound;

            application.ApplicationDate = request.ApplicationDate;

            await applicationRepository.Upsert(application);
            return application;
        }
    }
}
