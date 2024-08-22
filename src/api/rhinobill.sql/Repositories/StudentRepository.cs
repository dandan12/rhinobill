using rhinobill.core.Application.Students.Abstractions;
using rhinobill.core.Application.Students.Models;

namespace rhinobill.sql.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public StudentRepository(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task Delete(Guid id)
        {
            var entity = await context.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return;
            }

            context.Students.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<Student> Get(Guid id)
        {
            var entity = await context.Students.FirstOrDefaultAsync(x => x.Id == id);
            return mapper.Map<Student>(entity);
        }

        public async Task<Student[]> GetAll()
        {
            try
            {
                var entities = await context.Students.ToArrayAsync();
                return mapper.Map<Student[]>(entities);
            }
            catch (Exception ex)
            {
                throw;
            }
         
        }

        public async Task<Student> Upsert(Student student)
        {
            var entity = await context.Students.FirstOrDefaultAsync(x => x.Id == student.Id);
            if (entity == null)
            {
                entity = mapper.Map<StudentEntity>(student);
                context.Add(entity);
            }
            else
            {
                mapper.Map(student, entity);
                context.Update(entity);
            }

            await context.SaveChangesAsync();

            return mapper.Map<Student>(entity);
        }
    }
}
