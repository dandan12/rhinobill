namespace rhinobill.sql.Entities;

public class CourseEntity
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public int Credits { get; set; }
}
