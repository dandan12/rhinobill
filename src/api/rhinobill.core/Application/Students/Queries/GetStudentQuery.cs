using rhinobill.core.Application.Students.Abstractions;
using rhinobill.core.Application.Students.Models;
using rhinobill.core.Pipelines.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.core.Application.Students.Queries
{
    public record GetStudentQuery(Guid Id) : IQuery<Student>;
    public class GetStudentQueryHandler : IQueryHandler<GetStudentQuery, Student>
    {
        private readonly IStudentRepository studentRepository;

        public GetStudentQueryHandler(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task<Result<Student>> Handle(GetStudentQuery request, CancellationToken cancellationToken)
        {
            var student = await studentRepository.Get(request.Id);
            if (student is null) return ErrorResult.NotFound;

            return student;
        }
    }
}
