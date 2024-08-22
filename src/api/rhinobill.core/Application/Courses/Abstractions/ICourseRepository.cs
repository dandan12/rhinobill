using rhinobill.core.Application.Courses.Models;
using rhinobill.core.Application.Students.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.core.Application.Courses.Abstractions
{
    public interface ICourseRepository
    {
        Task<Course[]> GetAll();
        Task<Course> Get(Guid id);
        Task Upsert(Course student);
        Task Delete(Guid id);
    }
}
