using rhinobill.core.Application.Courses.Models;
using rhinobill.core.Application.Students.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.core.Application.Applications.Models
{
    public class Application
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime ApplicationDate { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
