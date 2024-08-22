using AutoBogus;
using rhinobill.api.Contracts.Requests.Applications;
using rhinobill.core.Application.Applications.Models;
using rhinobill.sql.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.component.tests.Data.Fakers
{
    public class CreateApplicationRequestFaker : AutoFaker<CreateApplicationRequest>
    {
        public CreateApplicationRequestFaker(Guid studentId, Guid courseId)
        {
            RuleFor(x => x.StudentId, studentId);
            RuleFor(x => x.CourseId, courseId);
        }
    }

    public class ApplicationFaker : AutoFaker<Application>
    {
        public ApplicationFaker(Guid studentId, Guid courseId)
        {
            RuleFor(x => x.StudentId, studentId);
            RuleFor(x => x.CourseId, courseId);
        }
    }

    public class ApplicationEntityFaker : AutoFaker<ApplicationEntity>
    {
        public ApplicationEntityFaker(Guid studentId, Guid courseId)
        {
            RuleFor(x => x.StudentId, studentId);
            RuleFor(x => x.CourseId, courseId);
        }
    }
}
