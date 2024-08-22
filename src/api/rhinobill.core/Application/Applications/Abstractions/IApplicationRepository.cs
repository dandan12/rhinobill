namespace rhinobill.core.Application.Applications.Abstractions
{
    public interface IApplicationRepository
    {
        Task<ApplicationModel[]> GetAll();
        Task<ApplicationModel> Get(Guid id);
        Task Upsert(ApplicationModel student);
        Task Delete(Guid id);
    }
}
