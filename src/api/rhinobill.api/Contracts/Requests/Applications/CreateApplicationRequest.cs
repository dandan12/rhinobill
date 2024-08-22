namespace rhinobill.api.Contracts.Requests.Applications
{
    public class CreateApplicationRequest
    {
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
}
