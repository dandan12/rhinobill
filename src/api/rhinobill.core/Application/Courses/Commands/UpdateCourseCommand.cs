using FluentValidation;
using rhinobill.core.Application.Courses.Abstractions;
using rhinobill.core.Application.Courses.Models;
using rhinobill.core.Pipelines.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.core.Application.Courses.Commands
{
    public record UpdateCourseCommand(Guid Id, string Code, string Title, int Credits) : ICommand<Course>;
    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator()
        {
            RuleFor(x => x.Code)
              .NotEmpty()
              .MinimumLength(5)
              .WithErrorCode(Errors.MinMaxLengthCode)
              .WithMessage(Errors.MinMaxLength510Message)
              .MaximumLength(10)
              .WithErrorCode(Errors.MinMaxLengthCode)
              .WithMessage(Errors.MinMaxLength510Message);

            RuleFor(x => x.Title)
                .NotEmpty()
                .MinimumLength(5)
                .WithErrorCode(Errors.MinMaxLengthCode)
                .WithMessage(Errors.MinMaxLength550Message)
                .MaximumLength(50)
                .WithErrorCode(Errors.MinMaxLengthCode)
                .WithMessage(Errors.MinMaxLength550Message);

            RuleFor(x => x.Credits)
                .NotEmpty()
                .GreaterThan(0)
                .WithErrorCode(Errors.CreditsMoreThanZeroCode)
                .WithMessage(Errors.CreditsMoreThanZeroMessage);
        }
    }

    public class UpdateCourseCommandHandler : ICommandHandler<UpdateCourseCommand, Course>
    {
        private readonly ICourseRepository courseRepository;

        public UpdateCourseCommandHandler(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public async Task<Result<Course>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await courseRepository.Get(request.Id);
            if (course is null) return ErrorResult.NotFound;

            course.Credits = request.Credits;
            course.Title = request.Title;
            course.Code = request.Code;

            await courseRepository.Upsert(course);
            return course;
        }
    }
}
