using rhinobill.core.Application.Courses.Abstractions;
using rhinobill.core.Application.Courses.Models;

namespace rhinobill.sql.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public CourseRepository(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task Delete(Guid id)
        {
            var entity = await context.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return;
            }

            context.Courses.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<Course> Get(Guid id)
        {
            var entity = await context.Courses.FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<Course>(entity);
        }

        public async Task<Course[]> GetAll()
        {
            var entities = await context.Courses.ToArrayAsync();
            return mapper.Map<Course[]>(entities);
        }

        public async Task Upsert(Course course)
        {
            var entity = await context.Courses.FirstOrDefaultAsync(x => x.Id == course.Id);
            if (entity == null)
            {
                entity = mapper.Map<CourseEntity>(course);
                context.Add(entity);
            }
            else 
            {
                mapper.Map(course, entity);
                context.Update(entity);
            }

            await context.SaveChangesAsync();
        }
    }
}
