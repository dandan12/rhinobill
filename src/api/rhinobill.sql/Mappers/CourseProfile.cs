using rhinobill.core.Application.Courses.Models;

namespace rhinobill.sql.Mappers
{
    internal class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<CourseEntity, Course>()
                .ReverseMap();
        }
    }
}
