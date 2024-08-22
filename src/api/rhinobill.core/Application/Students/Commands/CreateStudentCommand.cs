using FluentValidation;
using rhinobill.core.Application.Students.Abstractions;
using rhinobill.core.Application.Students.Models;
using rhinobill.core.Models.Common;
using rhinobill.core.Pipelines.Abstractions;

namespace rhinobill.core.Application.Students.Commands
{
    public record CreateStudentCommand(string FirstName, string LastName, DateOnly DateOfBirth, Email Email, string PhoneNumber) : ICommand<Student>;
    public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .MinimumLength(2)
                .WithErrorCode(Errors.MinMaxLengthCode)
                .WithMessage(Errors.MinMaxLength230Message)
                .MaximumLength(30)
                .WithErrorCode(Errors.MinMaxLengthCode)
                .WithMessage(Errors.MinMaxLength230Message);

            RuleFor(x => x.LastName)
                .MinimumLength(2)
                .WithErrorCode(Errors.MinMaxLengthCode)
                .WithMessage(Errors.MinMaxLength230Message)
                .MaximumLength(30)
                .WithErrorCode(Errors.MinMaxLengthCode)
                .WithMessage(Errors.MinMaxLength230Message);
        }
    }

    public class CreateStudentCommandHandler : ICommandHandler<CreateStudentCommand, Student>
    {
        private readonly IStudentRepository studentRepository;

        public CreateStudentCommandHandler(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task<Result<Student>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = new Student
            {
                Id = Guid.NewGuid(),
                DateOfBirth = request.DateOfBirth,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber
            };

            return await studentRepository.Upsert(student);
        }
    }
}
