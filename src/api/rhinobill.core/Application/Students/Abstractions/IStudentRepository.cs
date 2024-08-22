using rhinobill.core.Application.Students.Models;

namespace rhinobill.core.Application.Students.Abstractions
{
    public interface IStudentRepository
    {
        Task<Student[]> GetAll();
        Task<Student> Get(Guid id);
        Task<Student> Upsert(Student student);
        Task Delete(Guid id);
    }
}
