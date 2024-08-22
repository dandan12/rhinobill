namespace rhinobill.sql.Entities
{
    public class ApplicationEntity
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public DateTime ApplicationDate { get; set; }

        public virtual StudentEntity Student { get; set; }
        public virtual CourseEntity Course { get; set; }
    }
}
