using rhinobill.core.Pipelines.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using rhinobill.core.Application.Applications.Abstractions;
using rhinobill.core.Application.Students.Abstractions;
using rhinobill.core.Application.Courses.Abstractions;

namespace rhinobill.core.Application.Applications.Commands
{
    public record CreateApplicationCommand(Guid StudentId, Guid CourseId, DateTime ApplicationDate) : ICommand<ApplicationModel>;
    public class CreateApplicationCommandValidator : AbstractValidator<CreateApplicationCommand>
    {
        public CreateApplicationCommandValidator()
        {
            RuleFor(x => x.CourseId)
                .NotEmpty();

            RuleFor(x => x.StudentId)
                .NotEmpty();

            RuleFor(x => x.ApplicationDate)
                .NotEmpty();
        }
    }

    public class CreateApplicationCommandHandler(IApplicationRepository applicationRepository,
        IStudentRepository studentRepository,
        ICourseRepository courseRepository) : ICommandHandler<CreateApplicationCommand, ApplicationModel>
    {
        public async Task<Result<ApplicationModel>> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
        {
            var student = await studentRepository.Get(request.StudentId);
            if (student is null) return ErrorResult.NotFound;

            var course = await courseRepository.Get(request.CourseId);
            if (course is null) return ErrorResult.NotFound;

            var application = new ApplicationModel
            {
                Id = Guid.NewGuid(),
                CourseId = request.CourseId,
                StudentId = request.StudentId,
                ApplicationDate = request.ApplicationDate
            };

            await applicationRepository.Upsert(application);
            return application;
        }
    }
}
