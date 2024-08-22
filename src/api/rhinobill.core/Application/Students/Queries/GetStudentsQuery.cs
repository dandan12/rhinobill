using rhinobill.core.Application.Students.Abstractions;
using rhinobill.core.Application.Students.Models;
using rhinobill.core.Pipelines.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace rhinobill.core.Application.Students.Queries
{
    public record GetStudentsQuery() : IQuery<Student[]>;
    public class GetStudentsQueryHandler : IQueryHandler<GetStudentsQuery, Student[]>
    {
        private readonly IStudentRepository studentRepository;

        public GetStudentsQueryHandler(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task<Result<Student[]>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {
            return await studentRepository.GetAll();
        }
    }
}
