using rhinobill.core.Application.Applications.Abstractions;
using rhinobill.core.Application.Applications.Models;

namespace rhinobill.sql.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public ApplicationRepository(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task Delete(Guid id)
        {
            var entity = await context.Applications.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return;
            }

            context.Applications.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<Application> Get(Guid id)
        {
            var entity = await context.Applications.FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<Application>(entity);
        }

        public async Task<Application[]> GetAll()
        {
            var entities = await context.Applications.ToArrayAsync();
            return mapper.Map<Application[]>(entities);
        }

        public async Task Upsert(Application application)
        {
            var entity = await context.Applications.FirstOrDefaultAsync(x => x.Id == application.Id);
            if (entity == null)
            {
                entity = mapper.Map<ApplicationEntity>(application);
                context.Add(entity);
            }
            else
            {
                mapper.Map(application, entity);
                context.Update(entity);
            }

            await context.SaveChangesAsync();
        }
    }
}
