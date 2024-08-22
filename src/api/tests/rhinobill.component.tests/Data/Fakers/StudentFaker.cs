using AutoBogus;
using rhinobill.api.Contracts.Requests.Students;
using rhinobill.component.tests.Extensions;
using rhinobill.core.Application.Students.Models;
using rhinobill.core.Extensions;
using rhinobill.sql.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.component.tests.Data.Fakers
{
    public class CreateStudentRequestFaker : AutoFaker<CreateStudentRequest>
    {
        public CreateStudentRequestFaker()
        {
            RuleFor(x => x.DateOfBirth, (faker, x) => faker.Date.Past().ToDateOnly());
            RuleFor(x => x.Email, x => x.Person.SampleEmail());
        }
    }

    public class StudentFaker : AutoFaker<Student>
    {
        public StudentFaker()
        {
            RuleFor(x => x.DateOfBirth, (faker, x) => faker.Date.Past().ToDateOnly());
            RuleFor(x => x.Email, x => x.Person.SampleEmail());
        }
    }

    public class StudentEntityFaker : AutoFaker<StudentEntity>
    {
        public StudentEntityFaker()
        {
            RuleFor(x => x.DateOfBirth, (faker, x) => faker.Date.Past().ToDateOnly());
            RuleFor(x => x.Email, x => x.Person.SampleEmail());
        }
    }
}
