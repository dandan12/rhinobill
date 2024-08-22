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
    public record CreateCourseCommand(string Code, string Title, int Credits) : ICommand<Course>;
    public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseCommandValidator()
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

    public class CreateCourseCommandHandler(ICourseRepository courseRepository) : ICommandHandler<CreateCourseCommand, Course>
    {
        public async Task<Result<Course>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = new Course
            {
                Id = Guid.NewGuid(),
                Code = request.Code,
                Title = request.Title,
                Credits = request.Credits
            };

            await courseRepository.Upsert(course);
            return course;
        }
    }
}
