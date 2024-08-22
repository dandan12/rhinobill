using rhinobill.core.Application.Applications.Models;

namespace rhinobill.sql.Mappers
{
    internal class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<ApplicationEntity, Application>()
                .ReverseMap();
        }
    }
}
