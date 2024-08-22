using rhinobill.core.Application.Students.Abstractions;
using rhinobill.core.Pipelines.Abstractions;

namespace rhinobill.core.Application.Students.Commands
{
    public record DeleteStudentCommand(Guid Id) : ICommand<Guid>;
    public class DeleteStudentCommandHandler : ICommandHandler<DeleteStudentCommand, Guid>
    {
        private readonly IStudentRepository studentRepository;

        public DeleteStudentCommandHandler(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task<Result<Guid>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await studentRepository.Get(request.Id);
            if (student is null) return ErrorResult.NotFound;   

            await studentRepository.Delete(request.Id);

            return request.Id;
        }
    }
}
