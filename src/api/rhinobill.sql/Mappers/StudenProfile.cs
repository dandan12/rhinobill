using rhinobill.core.Application.Students.Models;

namespace rhinobill.sql.Mappers
{
    internal class StudenProfile : Profile
    {
        public StudenProfile()
        {
            CreateMap<StudentEntity, Student>()
                .ReverseMap();
        }
    }
}
