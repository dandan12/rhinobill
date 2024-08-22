using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.core.Application.Courses.Models
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
    }
}
