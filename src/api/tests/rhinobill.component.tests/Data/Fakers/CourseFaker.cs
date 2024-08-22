using AutoBogus;
using rhinobill.api.Contracts.Requests.Courses;
using rhinobill.core.Application.Courses.Models;
using rhinobill.sql.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.component.tests.Data.Fakers
{
    public class CreateCourseRequestFaker : AutoFaker<CreateCourseRequest>
    {
        public CreateCourseRequestFaker()
        {
            RuleFor(x => x.Code, "MIT123");
            RuleFor(x => x.Title, "BA MS PHD");
            RuleFor(x => x.Credits, 3);
        }
    }

    public class CourseFaker : AutoFaker<Course>
    {
        public CourseFaker()
        {
            RuleFor(x => x.Code, "MIT123");
            RuleFor(x => x.Title, "BA MS PHD");
            RuleFor(x => x.Credits, 3);
        }
    }

    public class CourseEntityFaker : AutoFaker<CourseEntity>
    {
        public CourseEntityFaker()
        {
            RuleFor(x => x.Code, "MIT123");
            RuleFor(x => x.Title, "BA MS PHD");
            RuleFor(x => x.Credits, 3);
        }
    }
}
