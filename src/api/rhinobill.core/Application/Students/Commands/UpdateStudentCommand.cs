using FluentValidation;
using rhinobill.core.Application.Students.Abstractions;
using rhinobill.core.Application.Students.Models;
using rhinobill.core.Models.Common;
using rhinobill.core.Pipelines.Abstractions;

namespace rhinobill.core.Application.Students.Commands
{
    public record UpdateStudentCommand(Guid Id, string FirstName, string LastName, DateOnly DateOfBirth, Email Email, string PhoneNumber) : ICommand<Student>;
    public class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
    {
        public UpdateStudentCommandValidator()
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

    public class UpdateStudentCommandHandler : ICommandHandler<UpdateStudentCommand, Student>
    {
        private readonly IStudentRepository studentRepository;

        public UpdateStudentCommandHandler(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task<Result<Student>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await studentRepository.Get(request.Id);
            if (student is null) return ErrorResult.NotFound;

            student.DateOfBirth = request.DateOfBirth;
            student.Email = request.Email;
            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.PhoneNumber = request.PhoneNumber;

            return await studentRepository.Upsert(student);
        }
    }
}
